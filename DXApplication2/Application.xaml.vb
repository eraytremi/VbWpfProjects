Imports Microsoft.Extensions.DependencyInjection

Class Application

    Private ServiceProvider As IServiceProvider

    Protected Overrides Sub OnStartup(e As StartupEventArgs)

        Dim services = New ServiceCollection()
        ConfigureServices(services)

        ServiceProvider = services.BuildServiceProvider()

    End Sub

    Private Sub ConfigureServices(services As IServiceCollection)
        services.AddSingleton(Of IApiService, ApiService)
    End Sub
End Class