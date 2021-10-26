using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Automate : MonoBehaviour
{
    public static List<string> moveList = new List<string>() { };
    public static List<string> soluceMoveList = new List<string>() { };
    private readonly List<string> allMoves = new List<string>()
    { "U", "D", "L", "R", "F", "B",
        "U2", "D2", "L2", "R2", "F2", "B2",
        "U'", "D'", "L'", "R'", "F'", "B'"
    };

    private CubeState cubeState;
    private ReadCube readCube;

    public InputField inputField;
    private bool anim = false;

    private void Awake()
    {
        Screen.SetResolution(1920, 1080, false);
    }

    private static void GetArg()
    {
        string mix = "";
        string solution = "";
        var args = System.Environment.GetCommandLineArgs();
        if (args.Length == 3)
        {
            mix = args[1];
            solution = args[2];
        }

        char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
        string[] words = mix.Split(delimiterChars);  

        List<string> moves = new List<string>();
        moves.AddRange(words);
        moveList = moves;

        words = solution.Split(delimiterChars);  

        moves = new List<string>();
        moves.AddRange(words);
        soluceMoveList = moves;
    }

    private void Start()
    {
        cubeState = FindObjectOfType<CubeState>();
        readCube = FindObjectOfType<ReadCube>();

        GetArg();
    }

    public void Solve()
    {
        Cleanup();
        moveList = soluceMoveList;
    }

    private void AnimationBegin()
    {
        anim = true;
        StartCoroutine(RotationAnimation());
    }

    IEnumerator RotationAnimation()
    {
        yield return new WaitForSeconds(.01f);
        if (moveList.Count > 0 && !CubeState.autoRotating && CubeState.started)
        {
            DoMove(moveList[0]);
            moveList.Remove(moveList[0]);
            anim = false;
        }
        anim = false;
    }

    public void Retry()
    {
        Cleanup();

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void Cleanup()
    {
        anim = false;
        CubeState.autoRotating = false;
        CubeState.started = true;
        moveList.Clear();
    }

    private void Update()
    {
        if (!anim)
            AnimationBegin();
    }

    public void Shuffle2()
    {
        //List<string> moves = new List<string>();
        
        string ift = inputField.text;

        //string[] stringSeparators = new string[] { ", " };
        //string[] splitArray =  ift.Split(stringSeparators, StringSplitOptions.None);

        char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
        string[] words = ift.Split(delimiterChars);  

        List<string> moves = new List<string>();

        //moves.AddRange(splitArray);
        moves.AddRange(words);

        moveList = moves;
    }

    public void Shuffle()
    {
        List<string> moves = new List<string>();
        int shuffleLength = UnityEngine.Random.Range(10, 30);
        for (int i = 0; i < shuffleLength; i++)
        {
            int randomMove = UnityEngine.Random.Range(0, allMoves.Count);
            moves.Add(allMoves[randomMove]);
        }
        moveList = moves;
    }

    private void DoMove(string move)
    {
        readCube.ReadState();
        CubeState.autoRotating = true;
        if (move == "U")
        {
            RotateSide(cubeState.up, -90);
        }
        if (move == "U'")
        {
            RotateSide(cubeState.up, 90);
        }
        if (move == "U2")
        {
            RotateSide(cubeState.up, -180);
        }
        if (move == "D")
        {
            RotateSide(cubeState.down, -90);
        }
        if (move == "D'")
        {
            RotateSide(cubeState.down, 90);
        }
        if (move == "D2")
        {
            RotateSide(cubeState.down, -180);
        }
        if (move == "L")
        {
            RotateSide(cubeState.left, -90);
        }
        if (move == "L'")
        {
            RotateSide(cubeState.left, 90);
        }
        if (move == "L2")
        {
            RotateSide(cubeState.left, -180);
        }
        if (move == "R")
        {
            RotateSide(cubeState.right, -90);
        }
        if (move == "R'")
        {
            RotateSide(cubeState.right, 90);
        }
        if (move == "R2")
        {
            RotateSide(cubeState.right, -180);
        }
        if (move == "F")
        {
            RotateSide(cubeState.front, -90);
        }
        if (move == "F'")
        {
            RotateSide(cubeState.front, 90);
        }
        if (move == "F2")
        {
            RotateSide(cubeState.front, -180);
        }
        if (move == "B")
        {
            RotateSide(cubeState.back, -90);
        }
        if (move == "B'")
        {
            RotateSide(cubeState.back, 90);
        }
        if (move == "B2")
        {
            RotateSide(cubeState.back, -180);
        }
    }

    private void RotateSide(List<GameObject> side, float angle)
    {
        PivotRotation pr = side[4].transform.parent.GetComponent<PivotRotation>();
        pr.StartAutoRotate(side, angle);
    }
}
