using NaughtyAttributes;
using UnityEngine;

public class InfrastructurePanelTest : MonoBehaviour
{
    [Required][SerializeField] InfrastructureObject infrastructureObject;
    [Required][SerializeField] InfrastructurePanel panel;
    [Required] [SerializeField] Infrastructure infrastructure;
    
    [Button]
    void TestInfrastructurePanel()
    {
        panel.Show(infrastructureObject);
    }
    public void OnInfrastructureClick(InfrastructureObject clickedInfrastructureObject)
    {
        panel.Hide();
        infrastructure.Sell(clickedInfrastructureObject);
    }
}