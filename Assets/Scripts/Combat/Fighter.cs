using System;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    [SerializeField] WeaponConfig defaultWeapon = null;
    [SerializeField] Transform rightHandTransform = null;
    [SerializeField] Transform leftHandTransform = null;
    [SerializeField] Animator anim;

    const string weaponName = "Unarmed";

    public static Fighter localPlayer;


    //Weapon
    WeaponConfig currentWeaponConfig;
    LazyValue<Weapon> currentWeapon;

    private void Awake()
    {
        currentWeaponConfig = defaultWeapon;
        currentWeapon = new LazyValue<Weapon>(SetupDefaultWeapon);
        currentWeapon.ForceInit();
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private Weapon SetupDefaultWeapon()
    {
        return AttachWeapon(defaultWeapon);
    }

    public bool AttackBehaviour()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetTrigger("attack");
            LookMousePosition();
            return true;
        }
        return false;
    }

    void Hit()
    {
        //Debug.Log("Yumruk Atıldı.");
    }

    private bool LookMousePosition()
    {
        float cameraRotation = Camera.main.transform.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0, cameraRotation, 0);
        if (transform.rotation == Quaternion.Euler(0, cameraRotation, 0))
        {
            return true;
        }
        return false;
    }

    public void EquipWeapon(WeaponConfig weapon)
    {
        currentWeaponConfig = weapon;
        currentWeapon.value = AttachWeapon(weapon);
    }

    public Weapon GetCurrentWeapon()
    {
        return currentWeapon.value;
    }

    private Weapon AttachWeapon(WeaponConfig weapon)
    {
        Animator animator = GetComponent<Animator>();
        return weapon.Spawn(rightHandTransform, leftHandTransform, animator);
    }
}
