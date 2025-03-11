using Bunit;
using LigaManagerManagement.Web.Pages;
using LigaManagement.Web.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Components;
using LigaManagement.Web.Pages;


public class SpieltageListBaseTests : TestContext
{
    private readonly Mock<ISaisonenService> _mockSaisonenService;
    private readonly Mock<ILigaService> _mockLigaService;
    private readonly Mock<ISpieltagService> _mockSpieltagService;
    private readonly Mock<IVereineService> _mockVereineService;
    private readonly Mock<IStringLocalizer<SpieltageList>> _mockLocalizer;

    public SpieltageListBaseTests()
    {
        _mockSaisonenService = new Mock<ISaisonenService>();
        _mockLigaService = new Mock<ILigaService>();
        _mockSpieltagService = new Mock<ISpieltagService>();
        _mockVereineService = new Mock<IVereineService>();
        _mockLocalizer = new Mock<IStringLocalizer<SpieltageList>>();

        Services.AddSingleton(_mockSaisonenService.Object);
        Services.AddSingleton(_mockLigaService.Object);
        Services.AddSingleton(_mockSpieltagService.Object);
        Services.AddSingleton(_mockVereineService.Object);
        Services.AddSingleton(_mockLocalizer.Object);
    }

    [Fact]
    public void ComponentRendersCorrectly()
    {
    //    // Arrange
    //    var cut = RenderComponent< SpieltagList();

    //    // Act
    //    // Assert
    //    cut.MarkupMatches("<div></div>"); // Adjust this to match the expected markup
    }

    [Fact]
    public async Task OnInitializedAsync_SetsPropertiesCorrectly()
    {
        //// Arrange
        //var cut = RenderComponent<SpieltagListBase>();

        //// Act
        //object initializationResult = await cut.Instance.OnInitializedAsync();

        //// Assert
        //Assert.NotNull(cut.Instance.SaisonenList);
        //Assert.NotNull(cut.Instance.Liganame);
    }

    [Fact]
    public async Task DisplaySpieltagAkt_SetsSpieltagListCorrectly()
    {
        // Arrange
        //var cut = RenderComponent<SpieltagListBase>();

        //// Act
        //await cut.Instance.DisplaySpieltagAkt();

        // Assert
        //Assert.NotNull(cut.Instance.SpieltagList);
        //Assert.True(cut.Instance.SpieltagList.Count > 0);
    }

    [Fact]
    public async Task SpieltagChange_UpdatesSpieltagNr()
    {
        // Arrange
        var cut = RenderComponent<SpieltagListBase>();
        var changeEventArgs = new ChangeEventArgs { Value = "2" };

        // Act
        await cut.Instance.SpieltagChange(changeEventArgs);

        // Assert
        Assert.Equal("2", cut.Instance.SpieltagNr);
    }

    [Fact]
    public async Task SpieltagZurueck_DecrementsSpieltagNr()
    {
        // Arrange
        var cut = RenderComponent<LigaManagerManagement.Web.Pages.SpieltagListBase>();
        
        cut.Instance.SpieltagNr = "2";

        // Act
        await cut.Instance.SpieltagZurueck();

        // Assert
        Assert.Equal("1", cut.Instance.SpieltagNr);
    }

    [Fact]
    public async Task SpieltagVor_IncrementsSpieltagNr()
    {
        // Arrange
        var cut = RenderComponent<LigaManagerManagement.Web.Pages.SpieltagListBase>();
        cut.Instance.SpieltagNr = "1";

        // Act
        await cut.Instance.SpieltagVor();

        // Assert
        Assert.Equal("2", cut.Instance.SpieltagNr);
    }
}
