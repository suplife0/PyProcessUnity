using System;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
public class PyExample01 : MonoBehaviour
{
    private Thread m_pyProcessThread;
    
    /// <summary>
    /// python.exe File Directory
    /// </summary>
    private string m_pythonDirectory = @"C:\Users\k3i\AppData\Local\Programs\Python\Python37\python.exe";
    
    /// <summary>
    /// .py File Directory
    /// </summary>
    private string m_pyDirectory = $"\"C:\\Users\\k3i\\Desktop\\Hack\\exp_code\\unity_sample.py\"";
    
    void Start()
    {
        StartThread();
    }
    
    // Start Thread
    public void StartThread()
    {
        m_pyProcessThread = new Thread(PyFunctionByProcessStartInfo);
        m_pyProcessThread.Start();
    }
    
    // Start Python File By Process
    public void PyFunctionByProcessStartInfo()
    {
        var psi = new ProcessStartInfo();
        psi.FileName = m_pythonDirectory;
        psi.Arguments = m_pyDirectory;

        psi.UseShellExecute = false;
        psi.CreateNoWindow = true;
        psi.RedirectStandardOutput = true;
        psi.RedirectStandardError = true;

        var erros = "";
        var results = "";
        
        Debug.Log("Start Python");

        using (var process = Process.Start(psi))
        {
            erros = process.StandardError.ReadToEnd();
            results = process.StandardOutput.ReadToEnd();
        }
        
        Debug.Log("ByProcessStartInfo");
        Debug.Log("Error : " + erros);
        Debug.Log("Results : " + results);
    }

    private void OnApplicationQuit()
    {
        if (m_pyProcessThread != null)
        {
            m_pyProcessThread.Abort();
        }
    }
}
