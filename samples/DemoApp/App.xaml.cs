﻿namespace DemoApp;

public partial class App : BurkusMvvmApplication
{
    public App()
    {
        InitializeComponent();
    }

#if NET7_0_OR_GREATER && !ANDROID && !MACCATALYST && !IOS && !WINDOWS
    // workaround for unit tests in Maui https://github.com/dotnet/maui/issues/3552#issuecomment-1172606125
    public static void Main(string[] args) {}
#endif
}