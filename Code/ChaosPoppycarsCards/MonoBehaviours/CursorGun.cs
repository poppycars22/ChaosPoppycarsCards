using Photon.Pun;
using SimulationChamber;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using System.Linq;

public class CursorGun : MonoBehaviour
{
        Player player;
        Gun gun;

        // A list of guns created for this mono saved here.
        // Ideally you'll make a pool of guns for your mod to use.
        public SimulatedGun[] savedGuns = new SimulatedGun[1];
    

    public static GameObject _stopRecursionObj = null;

    public static GameObject StopRecursionObj
    {
        get
        {
            if (_stopRecursionObj == null)
            {
                _stopRecursionObj = new GameObject("A_StopRecursion", typeof(StopRecursion));
                DontDestroyOnLoad(_stopRecursionObj);
            }
            return _stopRecursionObj;
        }
    }

    public static ObjectsToSpawn[] StopRecursionSpawn
    {
        get
        {
            return new ObjectsToSpawn[] { new ObjectsToSpawn() { AddToProjectile = StopRecursionObj } };
        }
    }

    public void Start()
    {
            // Get Player
            this.player = this.GetComponentInParent<Player>();
            // Get Gun
            this.gun = this.player.data.weaponHandler.gun;
            // Hook up our action.
            this.gun.ShootPojectileAction += this.OnShootProjectileAction;

            // Checks to see if we have a saved gun already, if not, we make one.
            if (savedGuns[0] == null)
            {
                // We spawn a new object since this allows us manipulate the gun object's position without messing with the player's gameobjest.
                savedGuns[0] = new GameObject("Cursor_One").AddComponent<SimulatedGun>();
            
            }
        
    }

    public void OnShootProjectileAction(GameObject obj)
    {
        if (obj.GetComponentsInChildren<StopRecursion>().Length > 0)
        {
            return;
        }

        SimulatedGun Cursor1 = savedGuns[0];

        // We copy over our gun stats, including actions, so that it's pretty much a copy of our gun.
        // Note, the methods for copying actions actually create separate instances of those actions
        Cursor1.CopyGunStatsExceptActions(this.gun);
        Cursor1.CopyAttackAction(this.gun);
        Cursor1.CopyShootProjectileAction(this.gun);

        // Since we created a separate instance of our shootprojectile action, we can safely remove this action
        // to avoid our simulated gun from triggering it as well.
        //
        // If we had simply done `xGun.ShootPojectileAction = this.gun.ShootPojectileAction;` this would have also
        // removed the action from `this.gun.ShootPojectileAction`.
        Cursor1.ShootPojectileAction -= this.OnShootProjectileAction;

        // We only want to fire 1 bullet per bullet, since we're mirroring our attacks.
        Cursor1.numberOfProjectiles = 1;
        Cursor1.bursts = 0;
        Cursor1.gravity = 0;
        Cursor1.damage *= 0.5f;
        Cursor1.objectsToSpawn = Cursor1.objectsToSpawn.Concat(StopRecursionSpawn).ToArray();




        /*************************************************************************
        **************************************************************************
        *** We check to see if the player who's shooting is that player, otherwise
        *** we'll end up firing a simulation gun for each player in the game.
        **************************************************************************
        *************************************************************************/
        if (!(player.data.view.IsMine || PhotonNetwork.OfflineMode))
        {
            return;
        }

        // Fires our gun that's mirrored across the y-axis, so we invert our x position and shoot angle.
        Cursor1.SimulatedAttack(this.player.playerID, new Vector3(MainCam.instance.cam.ScreenToWorldPoint(Input.mousePosition).x, MainCam.instance.cam.ScreenToWorldPoint(Input.mousePosition).y -0.5f, 0), new Vector3(0, -1, 0), 1f, 1);
        Cursor1.SimulatedAttack(this.player.playerID, new Vector3(MainCam.instance.cam.ScreenToWorldPoint(Input.mousePosition).x +0.5f, MainCam.instance.cam.ScreenToWorldPoint(Input.mousePosition).y, 0), new Vector3(1, 0, 0), 1f, 1);
        Cursor1.SimulatedAttack(this.player.playerID, new Vector3(MainCam.instance.cam.ScreenToWorldPoint(Input.mousePosition).x, MainCam.instance.cam.ScreenToWorldPoint(Input.mousePosition).y +0.5f, 0), new Vector3(0, 1, 0), 1f, 1);
        Cursor1.SimulatedAttack(this.player.playerID, new Vector3(MainCam.instance.cam.ScreenToWorldPoint(Input.mousePosition).x -0.5f, MainCam.instance.cam.ScreenToWorldPoint(Input.mousePosition).y, 0), new Vector3(-1, 0, 0), 1f, 1);
    }

    public void OnDestroy()
        {
            // Remove our action when the mono is removed
            gun.ShootPojectileAction -= OnShootProjectileAction;
        UnityEngine.GameObject.Destroy(savedGuns[0]);
    }
    }
