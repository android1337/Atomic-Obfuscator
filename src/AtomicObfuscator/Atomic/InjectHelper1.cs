// Decompiled with JetBrains decompiler
// Type: Atomic.InjectHelper1
// Assembly: AtomicObfuscator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B2B1F80D-5C6A-4DF3-B343-FE98A4C6DD2A
// Assembly location: C:\Users\liam_\Desktop\AtomicGay\Original\AtomicObfuscator.exe

using dnlib.DotNet;
using dnlib.DotNet.Emit;
using dnlib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Atomic
{
    internal class InjectHelper1
    {
        public static class InjectHelper
        {
            private static TypeDefUser Clone(TypeDef origin)
            {
                TypeDefUser typeDefUser = new TypeDefUser(origin.Namespace, origin.Name);
                typeDefUser.Attributes = origin.Attributes;
                if (origin.ClassLayout != null)
                    typeDefUser.ClassLayout = (ClassLayout)new ClassLayoutUser(origin.ClassLayout.PackingSize, origin.ClassSize);
                foreach (GenericParam genericParameter in (IEnumerable<GenericParam>)origin.GenericParameters)
                    typeDefUser.GenericParameters.Add((GenericParam)new GenericParamUser(genericParameter.Number, genericParameter.Flags, (UTF8String)"-"));
                return typeDefUser;
            }

            private static MethodDefUser Clone(MethodDef origin)
            {
                MethodDefUser methodDefUser = new MethodDefUser(origin.Name, (MethodSig)null, origin.ImplAttributes, origin.Attributes);
                foreach (GenericParam genericParameter in (IEnumerable<GenericParam>)origin.GenericParameters)
                    methodDefUser.GenericParameters.Add((GenericParam)new GenericParamUser(genericParameter.Number, genericParameter.Flags, (UTF8String)"-"));
                return methodDefUser;
            }

            private static FieldDefUser Clone(FieldDef origin)
            {
                return new FieldDefUser(origin.Name, (FieldSig)null, origin.Attributes);
            }

            private static TypeDef PopulateContext(
              TypeDef typeDef,
              InjectHelper1.InjectHelper.InjectContext ctx)
            {
                IDnlibDef dnlibDef;
                TypeDef typeDef1;
                if (!ctx.Map.TryGetValue((IDnlibDef)typeDef, out dnlibDef))
                {
                    typeDef1 = (TypeDef)InjectHelper1.InjectHelper.Clone(typeDef);
                    ctx.Map[(IDnlibDef)typeDef] = (IDnlibDef)typeDef1;
                }
                else
                    typeDef1 = (TypeDef)dnlibDef;
                foreach (TypeDef nestedType in (IEnumerable<TypeDef>)typeDef.NestedTypes)
                    typeDef1.NestedTypes.Add(InjectHelper1.InjectHelper.PopulateContext(nestedType, ctx));
                foreach (MethodDef method in (IEnumerable<MethodDef>)typeDef.Methods)
                    typeDef1.Methods.Add((MethodDef)(ctx.Map[(IDnlibDef)method] = (IDnlibDef)InjectHelper1.InjectHelper.Clone(method)));
                foreach (FieldDef field in (IEnumerable<FieldDef>)typeDef.Fields)
                    typeDef1.Fields.Add((FieldDef)(ctx.Map[(IDnlibDef)field] = (IDnlibDef)InjectHelper1.InjectHelper.Clone(field)));
                return typeDef1;
            }

            private static void CopyTypeDef(TypeDef typeDef, InjectHelper1.InjectHelper.InjectContext ctx)
            {
                TypeDef typeDef1 = (TypeDef)ctx.Map[(IDnlibDef)typeDef];
                typeDef1.BaseType = (ITypeDefOrRef)ctx.Importer.Import((IType)typeDef.BaseType);
                foreach (InterfaceImpl interfaceImpl in (IEnumerable<InterfaceImpl>)typeDef.Interfaces)
                    typeDef1.Interfaces.Add((InterfaceImpl)new InterfaceImplUser((ITypeDefOrRef)ctx.Importer.Import((IType)interfaceImpl.Interface)));
            }

            private static void CopyMethodDef(
              MethodDef methodDef,
              InjectHelper1.InjectHelper.InjectContext ctx)
            {
                MethodDef methodDef1 = (MethodDef)ctx.Map[(IDnlibDef)methodDef];
                methodDef1.Signature = ctx.Importer.Import(methodDef.Signature);
                methodDef1.Parameters.UpdateParameterTypes();
                if (methodDef.ImplMap != null)
                    methodDef1.ImplMap = (ImplMap)new ImplMapUser((ModuleRef)new ModuleRefUser(ctx.TargetModule, methodDef.ImplMap.Module.Name), methodDef.ImplMap.Name, methodDef.ImplMap.Attributes);
                foreach (CustomAttribute customAttribute in (LazyList<CustomAttribute>)methodDef.CustomAttributes)
                    methodDef1.CustomAttributes.Add(new CustomAttribute((ICustomAttributeType)ctx.Importer.Import((IMethod)customAttribute.Constructor)));
                if (!methodDef.HasBody)
                    return;
                methodDef1.Body = new CilBody(methodDef.Body.InitLocals, (IList<Instruction>)new List<Instruction>(), (IList<ExceptionHandler>)new List<ExceptionHandler>(), (IList<Local>)new List<Local>());
                methodDef1.Body.MaxStack = methodDef.Body.MaxStack;
                Dictionary<object, object> bodyMap = new Dictionary<object, object>();
                foreach (Local variable in methodDef.Body.Variables)
                {
                    Local local = new Local(ctx.Importer.Import(variable.Type));
                    methodDef1.Body.Variables.Add(local);
                    local.Name = variable.Name;
                    local.PdbAttributes = variable.PdbAttributes;
                    bodyMap[(object)variable] = (object)local;
                }
                foreach (Instruction instruction1 in (IEnumerable<Instruction>)methodDef.Body.Instructions)
                {
                    Instruction instruction2 = new Instruction(instruction1.OpCode, instruction1.Operand);
                    instruction2.SequencePoint = instruction1.SequencePoint;
                    if (instruction2.Operand is IType)
                        instruction2.Operand = (object)ctx.Importer.Import((IType)instruction2.Operand);
                    else if (instruction2.Operand is IMethod)
                        instruction2.Operand = (object)ctx.Importer.Import((IMethod)instruction2.Operand);
                    else if (instruction2.Operand is IField)
                        instruction2.Operand = (object)ctx.Importer.Import((IField)instruction2.Operand);
                    methodDef1.Body.Instructions.Add(instruction2);
                    bodyMap[(object)instruction1] = (object)instruction2;
                }
                foreach (Instruction instruction in (IEnumerable<Instruction>)methodDef1.Body.Instructions)
                {
                    if (instruction.Operand != null && bodyMap.ContainsKey(instruction.Operand))
                        instruction.Operand = bodyMap[instruction.Operand];
                    else if (instruction.Operand is Instruction[])
                        instruction.Operand = (object)((IEnumerable<Instruction>)(Instruction[])instruction.Operand).Select<Instruction, Instruction>((Func<Instruction, Instruction>)(target => (Instruction)bodyMap[(object)target])).ToArray<Instruction>();
                }
                foreach (ExceptionHandler exceptionHandler in (IEnumerable<ExceptionHandler>)methodDef.Body.ExceptionHandlers)
                    methodDef1.Body.ExceptionHandlers.Add(new ExceptionHandler(exceptionHandler.HandlerType)
                    {
                        CatchType = exceptionHandler.CatchType == null ? (ITypeDefOrRef)null : (ITypeDefOrRef)ctx.Importer.Import((IType)exceptionHandler.CatchType),
                        TryStart = (Instruction)bodyMap[(object)exceptionHandler.TryStart],
                        TryEnd = (Instruction)bodyMap[(object)exceptionHandler.TryEnd],
                        HandlerStart = (Instruction)bodyMap[(object)exceptionHandler.HandlerStart],
                        HandlerEnd = (Instruction)bodyMap[(object)exceptionHandler.HandlerEnd],
                        FilterStart = exceptionHandler.FilterStart == null ? (Instruction)null : (Instruction)bodyMap[(object)exceptionHandler.FilterStart]
                    });
                methodDef1.Body.SimplifyMacros((IList<Parameter>)methodDef1.Parameters);
            }

            private static void CopyFieldDef(
              FieldDef fieldDef,
              InjectHelper1.InjectHelper.InjectContext ctx)
            {
                ((FieldDef)ctx.Map[(IDnlibDef)fieldDef]).Signature = ctx.Importer.Import(fieldDef.Signature);
            }

            private static void Copy(
              TypeDef typeDef,
              InjectHelper1.InjectHelper.InjectContext ctx,
              bool copySelf)
            {
                if (copySelf)
                    InjectHelper1.InjectHelper.CopyTypeDef(typeDef, ctx);
                foreach (TypeDef nestedType in (IEnumerable<TypeDef>)typeDef.NestedTypes)
                    InjectHelper1.InjectHelper.Copy(nestedType, ctx, true);
                foreach (MethodDef method in (IEnumerable<MethodDef>)typeDef.Methods)
                    InjectHelper1.InjectHelper.CopyMethodDef(method, ctx);
                foreach (FieldDef field in (IEnumerable<FieldDef>)typeDef.Fields)
                    InjectHelper1.InjectHelper.CopyFieldDef(field, ctx);
            }

            public static TypeDef Inject(TypeDef typeDef, ModuleDef target)
            {
                InjectHelper1.InjectHelper.InjectContext ctx = new InjectHelper1.InjectHelper.InjectContext(typeDef.Module, target);
                InjectHelper1.InjectHelper.PopulateContext(typeDef, ctx);
                InjectHelper1.InjectHelper.Copy(typeDef, ctx, true);
                return (TypeDef)ctx.Map[(IDnlibDef)typeDef];
            }

            public static MethodDef Inject(MethodDef methodDef, ModuleDef target)
            {
                InjectHelper1.InjectHelper.InjectContext ctx = new InjectHelper1.InjectHelper.InjectContext(methodDef.Module, target);
                ctx.Map[(IDnlibDef)methodDef] = (IDnlibDef)InjectHelper1.InjectHelper.Clone(methodDef);
                InjectHelper1.InjectHelper.CopyMethodDef(methodDef, ctx);
                return (MethodDef)ctx.Map[(IDnlibDef)methodDef];
            }

            public static IEnumerable<IDnlibDef> Inject(
              TypeDef typeDef,
              TypeDef newType,
              ModuleDef target)
            {
                InjectHelper1.InjectHelper.InjectContext ctx = new InjectHelper1.InjectHelper.InjectContext(typeDef.Module, target);
                ctx.Map[(IDnlibDef)typeDef] = (IDnlibDef)newType;
                InjectHelper1.InjectHelper.PopulateContext(typeDef, ctx);
                InjectHelper1.InjectHelper.Copy(typeDef, ctx, false);
                return ctx.Map.Values.Except<IDnlibDef>((IEnumerable<IDnlibDef>)new TypeDef[1]
                {
          newType
                });
            }

            private class InjectContext : ImportResolver
            {
                public readonly Dictionary<IDnlibDef, IDnlibDef> Map = new Dictionary<IDnlibDef, IDnlibDef>();
                public readonly ModuleDef OriginModule;
                public readonly ModuleDef TargetModule;
                private readonly Importer importer;

                public InjectContext(ModuleDef module, ModuleDef target)
                {
                    this.OriginModule = module;
                    this.TargetModule = target;
                    this.importer = new Importer(target, ImporterOptions.TryToUseTypeDefs);
                    this.importer.Resolver = (ImportResolver)this;
                }

                public Importer Importer
                {
                    get
                    {
                        return this.importer;
                    }
                }

                public override TypeDef Resolve(TypeDef typeDef)
                {
                    return this.Map.ContainsKey((IDnlibDef)typeDef) ? (TypeDef)this.Map[(IDnlibDef)typeDef] : (TypeDef)null;
                }

                public override MethodDef Resolve(MethodDef methodDef)
                {
                    return this.Map.ContainsKey((IDnlibDef)methodDef) ? (MethodDef)this.Map[(IDnlibDef)methodDef] : (MethodDef)null;
                }

                public override FieldDef Resolve(FieldDef fieldDef)
                {
                    return this.Map.ContainsKey((IDnlibDef)fieldDef) ? (FieldDef)this.Map[(IDnlibDef)fieldDef] : (FieldDef)null;
                }
            }
        }
    }
}
