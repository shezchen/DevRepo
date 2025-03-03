using UnityEngine;

public class FrameRateManager : MonoBehaviour
{
    // 设置目标帧率
    [SerializeField]public int targetFrameRate { get; private set; } = 60;

    void Start()
    {
        // 固定游戏帧率
        Application.targetFrameRate = targetFrameRate;
    }
}