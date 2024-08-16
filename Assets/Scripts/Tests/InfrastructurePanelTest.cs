using NaughtyAttributes;
using UnityEngine;

public class InfrastructurePanelTest : MonoBehaviour
{
    [Required][SerializeField] InfrastructureObject infrastructure;
    [Required][SerializeField] InfrastructurePanel panel;
    
    [Button]
    void TestInfrastructurePanel()
    {
        panel.Show(infrastructure);
    }
}