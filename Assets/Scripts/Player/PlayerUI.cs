using UnityEngine;

public interface IPlayerUI
{
    void UpdateHP(int current, int max);

}

public class PlayerUI : MonoBehaviour, IPlayerUI
{
    [SerializeField] private ProgressBar _healthBar;

    public void SetPlayer(PlayerController player)
    {
        player.SetPlayerUI(this);
    }

    public void UpdateHP(int current, int max)
    {
        _healthBar.UpdateBar(current, max);
    }
}
