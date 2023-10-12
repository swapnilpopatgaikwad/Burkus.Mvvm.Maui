﻿namespace Burkus.Mvvm.Maui;

public interface INavigationService
{
    /// <summary>
    /// Push a new page onto the navigation stack.
    /// </summary>
    /// <typeparam name="T">Type of Page</typeparam>
    /// <returns>A completed task</returns>
    Task Push<T>()
        where T : Page;

    /// <summary>
    /// Push a new page onto the navigation stack.
    /// </summary>
    /// <typeparam name="T">Type of Page</typeparam>
    /// <param name="navigationParameters">Navigation parameters to pass</param>
    /// <returns>A completed task</returns>
    Task Push<T>(NavigationParameters navigationParameters)
        where T : Page;

    /// <summary>
    /// Pop the top page of the navigation stack.
    /// </summary>
    /// <returns>A completed task</returns>
    Task Pop();

    /// <summary>
    /// Pop the top page of the navigation stack.
    /// </summary>
    /// <param name="navigationParameters">Navigation parameters to pass</param>
    /// <returns>A completed task</returns>
    Task Pop(NavigationParameters navigationParameters);

    /// <summary>
    /// Pop to the root of the navigation stack.
    /// </summary>
    /// <returns>A completed task</returns>
    Task PopToRoot();

    /// <summary>
    /// Pop to the root of the navigation stack.
    /// </summary>
    /// <param name="navigationParameters">Navigation parameters to pass</param>
    /// <returns>A completed task</returns>
    Task PopToRoot(NavigationParameters navigationParameters);

    /// <summary>
    /// Replace the top page of the stack with a new page.
    /// </summary>
    /// <typeparam name="T">Type of Page</typeparam>
    /// <returns>A completed task</returns>
    Task ReplaceTopPage<T>()
        where T : Page;

    /// <summary>
    /// Replace the top page of the stack with a new page.
    /// </summary>
    /// <typeparam name="T">Type of Page</typeparam>
    /// <param name="navigationParameters">Navigation parameters to pass</param>
    /// <returns>A completed task</returns>
    Task ReplaceTopPage<T>(NavigationParameters navigationParameters)
        where T : Page;

    /// <summary>
    /// Reset the navigation stack and push a new page.
    /// </summary>
    /// <typeparam name="T">Type of Page</typeparam>
    /// <returns>A completed task</returns>
    Task ResetStackAndPush<T>()
        where T : Page;

    /// <summary>
    /// Reset the navigation stack and push a new page.
    /// </summary>
    /// <typeparam name="T">Type of Page</typeparam>
    /// <param name="navigationParameters">Navigation parameters to pass</param>
    /// <returns>A completed task</returns>
    Task ResetStackAndPush<T>(NavigationParameters navigationParameters)
        where T : Page;

    /// <summary>
    /// When within a <see cref="TabbedPage"/>, use this method to select a tab.
    /// </summary>
    /// <typeparam name="T">Type of Page</typeparam>
    void SelectTab<T>()
        where T : Page;

    /// <summary>
    /// Navigate using a string URI.
    /// </summary>
    /// <example>
    /// <code>
    /// // use absolute navigation to go to the LoginPage
    /// navigationService.Navigate("/LoginPage");
    /// 
    /// // push multiple pages relatively onto the stack
    /// navigationService.Navigate("RegisterPage/DemoTabsPage/RegisterPage");
    /// 
    /// // go back one page
    /// navigationService.Navigate("..");
    /// </code>
    /// </example>
    /// <param name="uri">The URI to navigate to</param>
    /// <remarks>This method is *very* experimental and likely to change.</remarks>
    /// <returns>A completed task</returns>
    Task Navigate(string uri);

    /// <summary>
    /// Navigate using a string URI.
    /// </summary>
    /// <example>
    /// <code>
    /// // use absolute navigation to go to the LoginPage
    /// navigationService.Navigate("/LoginPage");
    /// 
    /// // push multiple pages relatively onto the stack
    /// navigationService.Navigate("RegisterPage/DemoTabsPage/RegisterPage");
    /// 
    /// // go back one page
    /// navigationService.Navigate("..");
    /// </code>
    /// </example>
    /// <param name="uri">The URI to navigate to</param>
    /// <param name="navigationParameters">Navigation parameters to pass</param>
    /// <remarks>This method is *very* experimental and likely to change.</remarks>
    /// <returns>A completed task</returns>
    Task Navigate(string uri, NavigationParameters navigationParameters);
}
