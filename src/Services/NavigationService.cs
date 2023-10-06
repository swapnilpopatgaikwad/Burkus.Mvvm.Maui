﻿namespace Burkus.Mvvm.Maui;

internal class NavigationService : INavigationService
{
    public async Task Push<T>() where T : Page
    {
        var parameters = new NavigationParameters();
        await Push<T>(parameters);
    }

    public async Task Push<T>(NavigationParameters navigationParameters) where T : Page
    {
        await HandleNavigation<T>(async () =>
            {
                var pageToNavigateTo = ServiceResolver.Resolve<T>();

                if (navigationParameters.UseModalNavigation)
                {
                    await Application.Current.MainPage.Navigation.PushModalAsync(pageToNavigateTo, navigationParameters.UseAnimatedNavigation);
                }
                else
                {
                    await Application.Current.MainPage.Navigation.PushAsync(pageToNavigateTo, navigationParameters.UseAnimatedNavigation);
                }
            },
            navigationParameters);
    }

    public async Task Pop()
    {
        var parameters = new NavigationParameters();
        await Pop(parameters);
    }

    public async Task Pop(NavigationParameters navigationParameters)
    {
        await HandleNavigation<Page>(async () =>
            {
                if (navigationParameters.UseModalNavigation)
                {
                    _ = await Application.Current.MainPage.Navigation.PopModalAsync(navigationParameters.UseAnimatedNavigation);
                }
                else
                {
                    _ = await Application.Current.MainPage.Navigation.PopAsync(navigationParameters.UseAnimatedNavigation);
                }
            },
            navigationParameters);
    }

    public async Task PopToRoot()
    {
        var parameters = new NavigationParameters();
        await PopToRoot(parameters);
    }

    public async Task PopToRoot(NavigationParameters navigationParameters)
    {
        await HandleNavigation<Page>(async () =>
            {
                await Application.Current.MainPage.Navigation.PopToRootAsync(navigationParameters.UseAnimatedNavigation);
            },
            navigationParameters);
    }

    public async Task ReplaceTopPage<T>()
        where T : Page
    {
        var parameters = new NavigationParameters();
        await ReplaceTopPage<T>(parameters);
    }

    public async Task ReplaceTopPage<T>(NavigationParameters navigationParameters)
        where T : Page
    {
        await HandleNavigation<Page>(async () =>
            {
                var navigation = Application.Current.MainPage.Navigation;
                var pageToNavigateTo = ServiceResolver.Resolve<T>();

                navigation.InsertPageBefore(pageToNavigateTo, navigation.NavigationStack.Last());
                await navigation.PopAsync(navigationParameters.UseAnimatedNavigation);
            },
            navigationParameters);
    }

    public async Task ResetStackAndPush<T>()
        where T : Page
    {
        var parameters = new NavigationParameters();
        await ResetStackAndPush<T>(parameters);
    }

    public async Task ResetStackAndPush<T>(NavigationParameters navigationParameters)
        where T : Page
    {
        await HandleNavigation<Page>(async () =>
            {
                var navigation = Application.Current.MainPage.Navigation;
                var pageToNavigateTo = ServiceResolver.Resolve<T>();

                if (navigation.NavigationStack.Count > 0)
                {
                    // insert page as the new root page
                    navigation.InsertPageBefore(pageToNavigateTo, navigation.NavigationStack.Last());
                }
                else
                {
                    // the stack was already empty
                    await navigation.PushAsync(pageToNavigateTo, navigationParameters.UseAnimatedNavigation);
                }

                await navigation.PopToRootAsync(navigationParameters.UseAnimatedNavigation);
            },
            navigationParameters);
    }

    private async Task HandleNavigation<T>(Func<Task> navigationAction, NavigationParameters navigationParameters)
        where T : Page
    {
        var fromBindingContext = MauiPageUtility.GetTopPageBindingContext();
        var navigatingFromViewModel = fromBindingContext as INavigatingEvents;
        var navigatedFromViewModel = fromBindingContext as INavigatedEvents;

        if (navigatingFromViewModel != null)
        {
            await navigatingFromViewModel.OnNavigatingFrom(navigationParameters);
        }
        
        await navigationAction.Invoke();

        if (navigatedFromViewModel != null)
        {
            await navigatedFromViewModel.OnNavigatedFrom(navigationParameters);
        }

        var toBindingContext = MauiPageUtility.GetTopPageBindingContext();
        var navigatedToViewModel = toBindingContext as INavigatedEvents;

        if (navigatedToViewModel != null)
        {
            await navigatedToViewModel.OnNavigatedTo(navigationParameters);
        }
    }

    public async Task SelectTab<T>() where T : Page
    {
        var tabbedPage = MauiPageUtility.GetTopPage() as TabbedPage;

        if (tabbedPage == null)
        {
            // todo: warn about this in https://github.com/BurkusCat/Burkus.Mvvm.Maui/issues/17 ?
            return;
        }

        foreach (var child in tabbedPage.Children)
        {
            if (child.GetType() == typeof(T))
            {
                tabbedPage.CurrentPage = child;
                return;
            }

            if (child is NavigationPage)
            {
                if (((NavigationPage)child).CurrentPage.GetType() == typeof(T))
                {
                    tabbedPage.CurrentPage = child;
                    return;
                }
            }
        }
    }
}
