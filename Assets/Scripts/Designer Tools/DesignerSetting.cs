
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class DesignerSetting : EditorWindow
{
    int starter = 0;

    private GameObject Player;

    private PlayerMovement Player_script_movement;

    [SerializeField] float speed;

    private Interactor Player_script_interactor;
    [SerializeField] KeyCode interactionKey;


    [SerializeField] KeyCode snapShotKey;
    private Screenshot Player_script_Screenshot;

    [SerializeField] KeyCode explodeKey;
    private GameObject RockExit;
    private RockExit RockExit_script_RockExit;

    [SerializeField] bool enableCursor;
    [SerializeField] float mouseSensitivity;
    private GameObject CamFirstPerson;
    private ViewingControl CamFirstPerson_script_Viewcontrol;



    [MenuItem("My Tools/Designer Setting")]

    static void Init()
    {
        DesignerSetting window = (DesignerSetting)GetWindow(typeof(DesignerSetting));
        window.Show();

    }

    private void OnGUI()
    {
        Player = GameObject.Find("Player");
        Player_script_movement = Player.GetComponent<PlayerMovement>();
        Player_script_interactor = Player.GetComponent<Interactor>();
        Player_script_Screenshot = Player.GetComponent<Screenshot>();

        RockExit = GameObject.Find("Rock Exit");
        RockExit_script_RockExit = RockExit.GetComponent<RockExit>();

        CamFirstPerson = GameObject.Find("Camera FirstPerson");
        CamFirstPerson_script_Viewcontrol = CamFirstPerson.GetComponent<ViewingControl>();

        GUILayout.Label("Setup Variables", EditorStyles.boldLabel);
        GUILayout.Space(10);

        GUILayout.Label("Players");
        if (starter == 0)
        {
            speed = EditorGUILayout.FloatField("Speed", Player_script_movement.speed);
            enableCursor = EditorGUILayout.Toggle("Enable Cursor", CamFirstPerson_script_Viewcontrol.enableCursor);
            mouseSensitivity = EditorGUILayout.Slider("Mouse Sensitivity", CamFirstPerson_script_Viewcontrol.mouseSensitivity, 0, 2000);
            interactionKey = (KeyCode)EditorGUILayout.EnumPopup("Interaction Key", Player_script_interactor.interactionKey);
            snapShotKey = (KeyCode)EditorGUILayout.EnumPopup("Snap Shot Key", Player_script_Screenshot.snapShotKey);
            explodeKey = (KeyCode)EditorGUILayout.EnumPopup("Explosion Key", RockExit_script_RockExit.explodeKey);

            starter++;
        }
        else
        {

            speed = EditorGUILayout.FloatField("Speed", speed);
            enableCursor = EditorGUILayout.Toggle("Enable Cursor", enableCursor);
            mouseSensitivity = EditorGUILayout.Slider("Mouse Sensitivity", mouseSensitivity, 0, 2000);
            interactionKey = (KeyCode)EditorGUILayout.EnumPopup("Interaction Key", interactionKey);
            snapShotKey = (KeyCode)EditorGUILayout.EnumPopup("Snap Shot Key", snapShotKey);
            explodeKey = (KeyCode)EditorGUILayout.EnumPopup("Explosion Key", explodeKey);
        }


        if (GUILayout.Button("Set"))
        {
            Player_script_movement.speed = speed;
            CamFirstPerson_script_Viewcontrol.enableCursor = enableCursor;
            CamFirstPerson_script_Viewcontrol.mouseSensitivity = mouseSensitivity;
            Player_script_interactor.interactionKey = interactionKey;
            Player_script_Screenshot.snapShotKey = snapShotKey;
            RockExit_script_RockExit.explodeKey = explodeKey;
        }
    }

}
