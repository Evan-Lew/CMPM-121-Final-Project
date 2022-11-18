
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class DesignerSetting : EditorWindow
{

    private GameObject Player;
    private PlayerMovement Player_script_movement;
    [SerializeField] float speed;
    private Interactor Player_script_interactor;
    [SerializeField] KeyCode interactionKey;


    [MenuItem("My Tools/Designer Setting")]



    static void Init()
    {
        DesignerSetting window = (DesignerSetting)GetWindow(typeof(DesignerSetting));
        window.Show();
    }

    private void OnGUI()
    {

        GUILayout.Label("Setup Variables", EditorStyles.boldLabel);
        GUILayout.Space(10);

        GUILayout.Label("Players");
        speed = EditorGUILayout.FloatField("Speed", speed);
        interactionKey = (KeyCode)EditorGUILayout.EnumPopup("Interaction Key", interactionKey);


        if (GUILayout.Button("Set"))
        {
            Player = GameObject.Find("Player");
            Player_script_movement = Player.GetComponent<PlayerMovement>();
            Player_script_interactor = Player.GetComponent<Interactor>();
            Player_script_movement.speed = speed;
            Player_script_interactor.interactionKey = interactionKey;
        }
    }

}
