﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using System.Linq;
using System.Text;

namespace Burkus.Mvvm.Maui;

[Generator]
public class AppSourceGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        // register a syntax receiver that will process App class declarations
        context.RegisterForSyntaxNotifications(() => new AppReceiver());
    }

    public void Execute(GeneratorExecutionContext context)
    {
        if (context.SyntaxReceiver is not AppReceiver receiver)
            return;

        var semanticModel = context.Compilation.GetSemanticModel(receiver.AppClass.SyntaxTree);
        var isPartial = receiver.AppClass.Modifiers
                .Any(m => m.IsKind(SyntaxKind.PartialKeyword));

        // check if the App class is partial and inherits from Application
        var appSymbol = semanticModel.GetDeclaredSymbol(receiver.AppClass);
        if (appSymbol is null || !isPartial || !appSymbol.BaseType.Equals(semanticModel.Compilation.GetTypeByMetadataName("Application")))
            return;

        // generate the source code for the CreateWindow method
        var sourceBuilder = """
                            using System;
                            partial class App
                            {
                                protected override Window CreateWindow(IActivationState? activationState)
                                {
                                    Current.MainPage = new NavigationPage();

                                    var burkusMvvmBuilder = ServiceResolver.Resolve<IBurkusMvvmBuilder>();
                                    var navigationService = ServiceResolver.Resolve<INavigationService>();
                                    var serviceProvider = ServiceResolver.GetServiceProvider();

                                    // perform the user's desired initialization logic
                                    if (burkusMvvmBuilder.onStartFunc != null)
                                    {
                                        burkusMvvmBuilder.onStartFunc.Invoke(navigationService, serviceProvider);
                                    }

                                    return base.CreateWindow(activationState);
                                }
                            }
                            """;

        // add the generated source file to the compilation
        context.AddSource("AppGenerator", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
    }
}

