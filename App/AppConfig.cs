// 
// Current member / type: loader.App.AppConfig
// File path: C:\Users\Admin\Documents\loader\App\AppConfig.cs
// 
// Product version: 2019.1.118.0
// Object reference not set to an instance of an object.
//    at Mono.Cecil.Cil.CodeReader.IsInSection(Int32 rva) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Mono.Cecil\Mono.Cecil.Cil\CodeReader.cs:line 65
//    at Mono.Cecil.Cil.CodeReader.MoveTo(Int32 rva) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Mono.Cecil\Mono.Cecil.Cil\CodeReader.cs:line 55
//    at Mono.Cecil.Cil.CodeReader.ReadMethodBody() in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Mono.Cecil\Mono.Cecil.Cil\CodeReader.cs:line 70
//    at Mono.Cecil.Cil.CodeReader.ReadMethodBody(MethodDefinition method) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Mono.Cecil\Mono.Cecil.Cil\CodeReader.cs:line 48
//    at Mono.Cecil.MetadataReader.ReadMethodBody(MethodDefinition method) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Mono.Cecil\Mono.Cecil\AssemblyReader.cs:line 2139
//    at Mono.Cecil.MethodDefinition.<>c.<get_Body>b__40_0(MethodDefinition method, MetadataReader reader) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Mono.Cecil\Mono.Cecil\MethodDefinition.cs:line 168
//    at Mono.Cecil.ModuleDefinition.Read[TItem,TRet](TRet& variable, TItem item, Func`3 read) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Mono.Cecil\Mono.Cecil\ModuleDefinition.cs:line 906
//    at Mono.Cecil.MethodDefinition.get_Body() in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Mono.Cecil\Mono.Cecil\MethodDefinition.cs:line 168
//    at ..(MethodDefinition , & , Boolean , Func`2 ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Decompiler\PropertyDecompiler.cs:line 137
//    at ..(& , & , Boolean& ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Decompiler\PropertyDecompiler.cs:line 117
//    at ..(TypeDefinition , ILanguage ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs:line 393
//    at ..(TypeDefinition , ILanguage ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Decompiler\WriterContextServices\TypeCollisionWriterContextService.cs:line 84
//    at ..(IMemberDefinition , ILanguage ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Decompiler\WriterContextServices\TypeCollisionWriterContextService.cs:line 25
//    at ..(TypeDefinition ,  ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Languages\NamespaceImperativeLanguageWriter.cs:line 35
//    at JustDecompile.Tools.MSBuildProjectBuilder.BaseProjectBuilder.WriteTypeToFile(TypeDefinition type,  itemWriter, Dictionary`2 membersToSkip, Boolean shouldBePartial, ILanguage language, List`1& writingInfos, String& theCodeString) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\MSBuildProjectCreator\BaseProjectBuilder.cs:line 925
