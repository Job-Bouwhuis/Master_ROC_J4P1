// Creator: Job

namespace ShadowUprising.WorldInteraction
{
    /// <summary>
    /// Allows for the player to interact with this component.
    /// <br></br>
    /// <br></br><b>NOTE:</b> This will need a <see cref="UnityEngine.Collider"/> to be present on the object for the interaction system to work on this object.
    /// </summary>
    public interface IWorldInteractable
    {
        /// <summary>
        /// When implemented, will allow the player to interact with the object.
        /// </summary>
        /// <param name="interactor">The calling interactor</param>
        void Interact(WorldInteractor interactor);
    }
}