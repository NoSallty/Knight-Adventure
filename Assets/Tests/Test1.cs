using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

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

        startButton.onClick.Invoke();

        yield return new WaitForSeconds(5f);

        // Kiểm tra xem Scene đã chuyển đổi thành Game Scene
        Assert.AreEqual("StoryLine", SceneManager.GetActiveScene().name, "Chưa chuyển đến Game Scene.");
    }

    [UnityTest]
    public IEnumerator Test_OptionsButton_OpensOptionsMenu()
    {
        // Giả lập nhấp vào nút Options
        var optionsButton = mainMenu.transform.Find("Continue").GetComponent<Button>();
        optionsButton.onClick.Invoke();

        yield return new WaitForSeconds(5f);

        // Kiểm tra xem menu Options đã mở
        var optionsMenu = SceneManager.GetActiveScene().name;
        Assert.IsNotNull(optionsMenu, "Options Menu không được tìm thấy.");
        Assert.IsTrue(optionsMenu.Equals("LevelSelect"), "Options Menu phải hiển thị.");
    }

    [UnityTest]
    public IEnumerator Test_ExitButton_ClosesGame()
    {
        // Giả lập nhấp vào nút Exit
        var exitButton = mainMenu.transform.Find("Exit game").GetComponent<Button>();
        UnityEngine.Debug.Log(exitButton);
        Assert.IsNotNull(exitButton, "Nút 'Exit game' không được tìm thấy.");
        exitButton.onClick.Invoke();

        yield return new WaitForSeconds(5f);

        // Kiểm tra xem trò chơi đã thoát
        Assert.IsFalse(Application.isPlaying, "Trò chơi không thoát.");
    }

    [TearDown]
    public void Teardown()
    {
        // Dọn dẹp sau mỗi bài kiểm thử nếu cần
    }
}

public class GameStateManager : MonoBehaviour
{
    public static bool IsGameStarted { get; private set; }

    public void StartGame()
    {
        IsGameStarted = true;
        // Bắt đầu game logic ở đây
    }
}