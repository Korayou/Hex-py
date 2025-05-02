using UnityEngine;
using System.Diagnostics;
using System;
using Debug = UnityEngine.Debug;
using System.Collections.Generic;

public class IA
{
    public enum Behaviour { RUN, LEARN };
    private Dictionary<Behaviour, string> BehaviourDict = new Dictionary<Behaviour, string>();

    public IA() {
        BehaviourDict.Add(Behaviour.RUN, "-run");
        BehaviourDict.Add(Behaviour.LEARN, "-learn");
    }

    public void Execute(Behaviour behaviour, string input, string output, bool virus)
    {
        string stdout = "";
        string stderr = "";

        string strBehaviour = BehaviourDict[behaviour];

        Process process = new Process();
        process.StartInfo.FileName = "java"; // Chemin vers java.exe ou le binaire Java
        process.StartInfo.Arguments = $"-jar MLPerceptron.jar {strBehaviour} -m model.mlp -i {input} -o {output}"; // Arguments pour la JVM

        process.StartInfo.UseShellExecute = false; // Important pour rediriger la sortie
        process.StartInfo.RedirectStandardOutput = true; // Rediriger la sortie standard
        process.StartInfo.RedirectStandardError = true; // Rediriger la sortie d'erreur
        process.StartInfo.CreateNoWindow = !virus; // Emp�cher l'affichage d'une fen�tre de console

        try
        {
            process.Start();
            stdout = process.StandardOutput.ReadToEnd();
            stderr = process.StandardError.ReadToEnd();
            process.WaitForExit(); // Attendre la fin du processus
            //if (behaviour == Behaviour.LEARN)
            //    Debug.Log(stdout);
        }
        catch (Exception e)
        {
            stderr = $"Erreur lors de l'ex�cution de la commande Java : {e.Message}";
        }

        if (!string.IsNullOrEmpty(stderr))
        {
            Debug.LogError(stderr);
        }
    }
}
