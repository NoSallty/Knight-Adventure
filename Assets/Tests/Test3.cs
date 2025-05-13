using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

[TestFixture]
public class GamePerformanceTests
{
    private float targetFPS = 60f;
    private long initialMemory;

    [UnityTest]
    public IEnumerator TestFPSStability()
    {
        SceneManager.LoadScene("Level3"); 
        yield return new WaitForSeconds(1); 

        float fps = 0;
        for (int i = 0; i < 10; i++)
        {
            fps = 1.0f / Time.deltaTime;
            Assert.IsTrue(fps >= targetFPS, "FPS thấp hơn 60.");
            yield return null; // Đợi khung hình tiếp theo
        }
    }

    [UnityTest]
    public IEnumerator TestCPUAndGPUUsage()
    {
        SceneManager.LoadScene("Level3");  
        yield return new WaitForSeconds(2); 

        float cpuUsage = SystemInfo.processorFrequency;
        float gpuUsage = SystemInfo.graphicsMemorySize; 

        Assert.IsTrue(cpuUsage < 80, "Mức sử dụng CPU vượt quá giới hạn.");
        Assert.IsTrue(gpuUsage < 80, "Mức sử dụng GPU vượt quá giới hạn.");
    }

    [UnityTest]
    public IEnumerator TestMemoryUsage()
    {
        initialMemory = System.GC.GetTotalMemory(true);
        SceneManager.LoadScene("Level3"); 
        yield return new WaitForSeconds(2); 

        long currentMemory = System.GC.GetTotalMemory(true);
        Assert.IsTrue(currentMemory - initialMemory < 100 * 1024 * 1024, "Mức tiêu thụ RAM quá cao.");
    }
}
