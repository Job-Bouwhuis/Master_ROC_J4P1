using ShadowUprising.DeathSaves;
using ShadowUprising.UI;
using ShadowUprising.UI.Loading;
using WinterRose;

public class ReloadLastSave : ButtonFunction
{
    public override void Invoke(TextButton button)
    {
        if(LoadingScreen.Instance == null)
        {
            Windows.MessageBox("No Loading screen in the scene. Cant load");
            return;
        }

        string currentScene = LoadingScreen.Instance.CurrentScene;

        DeathSaveManager.Instance.IsResetting = true;
        LoadingScreen.Instance.ShowAndLoad(currentScene);
    }
}
