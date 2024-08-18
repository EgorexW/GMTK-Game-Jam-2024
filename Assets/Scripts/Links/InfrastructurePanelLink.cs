using NaughtyAttributes;
using UnityEngine;

public class InfrastructurePanelLink : MonoBehaviour
{
    [Required][SerializeField] InfrastructurePanel panel;
    
    Infrastructure infrastructure;
    
    public void OnInfrastructureClicked(InfrastructureObject infrastructureObject)
    {
        panel.Show(infrastructureObject);
    }
    public void OnInfrastructurePanelClick(InfrastructureObject clickedInfrastructureObject)
    {
        infrastructure.Sell(clickedInfrastructureObject);
        panel.Hide();
    }

    public void SetInfrastructure(Infrastructure newInfrastructure)
    {
        infrastructure = newInfrastructure;
    }
}