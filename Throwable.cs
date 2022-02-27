using UnityEngine;

public interface Throwable    //scripts with Trigger, will inherit Shoot() function
{
    void ReleaseProjectile();
    void HideModel();
    void ShowModel();
}
//Every weapon has a shoot function, but the content of that Shoot function can differ