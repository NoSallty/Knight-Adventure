using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using NUnit.Framework;

[TestFixture]
public class LevelMenuTests
{
    private Canvas menuLevel;

    [UnitySetUp]
    public IEnumerator Setup()
    {
        // Khởi động Scene chứa Main Menu
        SceneManager.LoadScene("LevelSelect", LoadSceneMode.Single);
        UnityEngine.Debug.Log(SceneManager.GetActiveScene().name);

        // Chờ một vài giây
        yield return new WaitForSeconds(5f); // Chờ 2 giây

        menuLevel = GameObject.Find("LevelSelect").GetComponent<Canvas>(); // Lấy Canvas
        UnityEngine.Debug.Log(menuLevel != null ? "Đã tìm thấy menuLevel!" : "Không tìm thấy menuLevel.");
    }

    [Test]
    public void Test_menuLevel_DisplaysCorrectly()
    {
        Assert.IsNotNull(menuLevel, "Không được tìm thấy.");
        Assert.IsTrue(menuLevel.gameObject.activeSelf, "Phải hiển thị khi trò chơi khởi động.");
    }

    [UnityTest]
    public IEnumerator Test_StartButton_Level1()
    {
        // Giả lập nhấp vào nút Start
        var startButton = menuLevel.transform.Find("Level1").GetComponent<Button>();

        startButton.onClick.Invoke();

        yield return new WaitForSeconds(5f);

        // Kiểm tra xem Scene đã chuyển đổi thành Game Scene
        Assert.AreEqual("Level1", SceneManager.GetActiveScene().name, "Chưa chuyển đến Game Scene.");
    }

    [UnityTest]
    public IEnumerator Test_StartButton_Level2()
    {
        // Giả lập nhấp vào nút Start
        var startButton = menuLevel.transform.Find("Level2").GetComponent<Button>();

        startButton.onClick.Invoke();

        yield return new WaitForSeconds(5f);

        // Kiểm tra xem Scene đã chuyển đổi thành Game Scene
        Assert.AreEqual("Level2", SceneManager.GetActiveScene().name, "Chưa chuyển đến Game Scene.");
    }

    [UnityTest]
    public IEnumerator Test_StartButton_Level3()
    {
        // Giả lập nhấp vào nút Start
        var startButton = menuLevel.transform.Find("Level3").GetComponent<Button>();

        startButton.onClick.Invoke();

        yield return new WaitForSeconds(5f);

        // Kiểm tra xem Scene đã chuyển đổi thành Game Scene
        Assert.AreEqual("Level3", SceneManager.GetActiveScene().name, "Chưa chuyển đến Game Scene.");
    }

    [UnityTest]
    public IEnumerator Test_StartButton_Level4()
    {
        // Giả lập nhấp vào nút Start
        var startButton = menuLevel.transform.Find("Level4").GetComponent<Button>();

        startButton.onClick.Invoke();

        yield return new WaitForSeconds(5f);

        // Kiểm tra xem Scene đã chuyển đổi thành Game Scene
        Assert.AreEqual("Level4", SceneManager.GetActiveScene().name, "Chưa chuyển đến Game Scene.");
    }

    [UnityTest]
    public IEnumerator Test_StartButton_Level5()
    {
        // Giả lập nhấp vào nút Start
        var startButton = menuLevel.transform.Find("Level5").GetComponent<Button>();

        startButton.onClick.Invoke();

        yield return new WaitForSeconds(5f);

        // Kiểm tra xem Scene đã chuyển đổi thành Game Scene
        Assert.AreEqual("Level5", SceneManager.GetActiveScene().name, "Chưa chuyển đến Game Scene.");
    }

    [UnityTest]
    public IEnumerator Test_StartButton_Boss()
    {
        // Giả lập nhấp vào nút Start
        var startButton = menuLevel.transform.Find("Boss").GetComponent<Button>();

        startButton.onClick.Invoke();

        yield return new WaitForSeconds(5f);

        // Kiểm tra xem Scene đã chuyển đổi thành Game Scene
        Assert.AreEqual("Boss", SceneManager.GetActiveScene().name, "Chưa chuyển đến Game Scene.");
    }

    [UnityTest]
    public IEnumerator Test_StartButton_Extra()
    {
        // Giả lập nhấp vào nút Start
        var startButton = menuLevel.transform.Find("Extra").GetComponent<Button>();

        startButton.onClick.Invoke();

        yield return new WaitForSeconds(5f);

        // Kiểm tra xem Scene đã chuyển đổi thành Game Scene
        Assert.AreEqual("Extra", SceneManager.GetActiveScene().name, "Chưa chuyển đến Game Scene.");
    }

    [UnityTest]
    public IEnumerator Test_StartButton_MainMenu()
    {
        // Giả lập nhấp vào nút Start
        var startButton = menuLevel.transform.Find("Exit game").GetComponent<Button>();

        startButton.onClick.Invoke();

        yield return new WaitForSeconds(5f);

        // Kiểm tra xem Scene đã chuyển đổi thành Game Scene
        Assert.AreEqual("MainMenu", SceneManager.GetActiveScene().name, "Chưa chuyển đến Game Scene.");
    }
}
