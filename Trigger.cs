using UnityEngine;

public interface Trigger    //scripts with Trigger, will inherit Shoot() function
{
    void Shoot();
}
//Every weapon has a shoot function, but the content of that Shoot function can differ