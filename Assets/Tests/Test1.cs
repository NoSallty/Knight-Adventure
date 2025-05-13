using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using NUnit.Framework;

[TestFixture]
public class MainMenuTests
{
    private Canvas mainMenu;

    [UnitySetUp]
    public IEnumerator Setup()
    {
        // Khởi động Scene chứa Main Menu
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        UnityEngine.Debug.Log(SceneManager.GetActiveScene().name);

        // Chờ một vài giây
        yield return new WaitForSeconds(5f); // Chờ 2 giây

        mainMenu = GameObject.Find("MainMenu").GetComponent<Canvas>(); // Lấy Canvas
        UnityEngine.Debug.Log(mainMenu != null ? "Đã tìm thấy MainMenu!" : "Không tìm thấy MainMenu.");
    }

    [Test]
    public void Test_MainMenu_DisplaysCorrectly()
    {
        Assert.IsNotNull(mainMenu, "Main Menu không được tìm thấy.");
        Assert.IsTrue(mainMenu.gameObject.activeSelf, "Main Menu phải hiển thị khi trò chơi khởi động.");
    }

    [UnityTest]
    public IEnumerator Test_StartButton_OpensGameScene()
    {
        // Giả lập nhấp vào nút Start
        var startButton = mainMenu.transform.Find("New game").GetComponent<Button>();
        Assert.IsNotNull(startButton, "Nút 'New game' không được tìm thấy.");
        startButton.onClick.Invoke();

        yield return new WaitForSeconds(5f); // Chờ một chút để Scene chuyển đổi

        // Kiểm tra xem Scene đã chuyển đổi thành Game Scene
        Assert.AreEqual("StoryLine", SceneManager.GetActiveScene().name, "Chưa chuyển đến Game Scene.");
    }

    [UnityTest]
    public IEnumerator Test_LevelSelectButton_OpensLevelSelectMenu()
    {
        // Giả lập nhấp vào nút Options
        var optionsButton = mainMenu.transform.Find("LevelSelect").GetComponent<Button>();
        Assert.IsNotNull(optionsButton, "Nút 'LevelSelect' không được tìm thấy.");
        optionsButton.onClick.Invoke();

        yield return new WaitForSeconds(5f); // Chờ một chút để Scene chuyển đổi

        // Kiểm tra xem menu đã mở
        var optionsMenu = GameObject.Find("LevelSelect");
        Assert.IsNotNull(optionsMenu, "Level Select không được tìm thấy.");
        Assert.IsTrue(optionsMenu.activeSelf, "Level Select phải hiển thị.");
    }

    [UnityTest]
    public IEnumerator Test_ExitButton_ClosesGame()
    {
        // Giả lập nhấp vào nút Exit
        var exitButton = mainMenu.transform.Find("Exit game").GetComponent<Button>();
        Assert.IsNotNull(exitButton, "Nút 'Exit game' không được tìm thấy.");
        exitButton.onClick.Invoke();

        yield return new WaitForSeconds(5f); // Chờ một chút để Scene chuyển đổi

        // Kiểm tra xem trò chơi đã thoát
        Assert.IsFalse(Application.isPlaying, "Trò chơi không thoát.");
    }

}