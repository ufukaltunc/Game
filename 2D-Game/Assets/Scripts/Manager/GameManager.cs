using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
public class GameManager : MonoBehaviour
{
    private Transform respawnPoint;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float respawnTime;
    private CinemachineVirtualCamera CVC;
    [SerializeField]
    private TextMeshProUGUI pointsForCoin, pointsForChest;
    private PlayerStats Ps;
    private HealthBar healthBar;

    private float respawnTimeStart;
    private bool respawn;

    private int totalpoint;
    private int totalChest;

    private void Start()
    {
        CVC = GameObject.Find("Player Camera").GetComponent<CinemachineVirtualCamera>();
        pointsForCoin = GameObject.Find("UICanvas/pointsForCoin").GetComponent<TextMeshProUGUI>();
        pointsForChest = GameObject.Find("UICanvas/pointsForChest").GetComponent<TextMeshProUGUI>();
        respawnPoint = GameObject.Find("GameManager/Respawn Point").transform;
        Ps = FindObjectOfType<PlayerStats>();
        healthBar = FindObjectOfType<HealthBar>();
        totalpoint = 0;
        totalChest = 0;
        pointsForCoin.text = totalpoint.ToString();
    }
    private void Update()
    {
        CheckRespawn();
    }
    public void Respawn()
    {
        respawnTimeStart = Time.time;
        respawn = true;
    }
    private void CheckRespawn()
    {

        if (Time.time >= respawnTimeStart + respawnTime && respawn)
        {
            var playerTemp = Instantiate(player, respawnPoint);
            CVC.m_Follow = playerTemp.transform;
            Ps = FindObjectOfType<PlayerStats>();
            healthBar.SetHealth(Ps.currentHealth);
            respawn = false;
        }
    }
    public void CoinAddToScore(int pointsForCoinPickup)
    {
        totalpoint += pointsForCoinPickup;
        pointsForCoin.text = totalpoint.ToString();
    }
    public void ChestAddToScore(int pointsForChestPickup)
    {
        totalChest += pointsForChestPickup;
        pointsForChest.text = totalChest.ToString() + "/8";
    }
    public void NewRespawnPoint(Transform newResPoint)
    {
        respawnPoint = newResPoint;
    }
    public void GetHeart(GameObject heart)
    {
        if (Ps.currentHealth != Ps.maxHealth)
        {
            Ps.IncreaseHealth(10.0f);
            Destroy(heart);
        }
    }
    public void Health(float amount)
    {
        healthBar.SetHealth(amount);
    }
}
