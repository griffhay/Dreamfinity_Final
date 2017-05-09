using UnityEditor;
using UnityEngine;

public class EZ_Style : Editor
{
	public static Rect rect;

	public static bool Header(string title, bool useToggle, bool toggle)
	{
		rect = EditorGUILayout.BeginVertical();
		GUI.Box(rect, GUIContent.none);
		EditorGUILayout.Space();

		if(useToggle)
			toggle = EditorGUILayout.ToggleLeft(title, toggle, EditorStyles.boldLabel);
		else
			EditorGUILayout.LabelField(title, EditorStyles.boldLabel);

		EditorGUILayout.Space();
		return toggle;
	}

	public static void Footer() 
	{ 		
		EditorGUILayout.Space();
		EditorGUILayout.EndVertical(); 
	}

	public static void BeginRectBox()
	{
		rect = EditorGUILayout.BeginVertical();
		GUI.Box(rect, GUIContent.none);
	}

	public static void EndRectBox()
	{
		EditorGUILayout.EndVertical();
	}

	public static void DisplayInformation(string forumLink)
	{
		BeginRectBox();
		Header("Information", false, false);

		GUILayout.Space(10);

		if(GUILayout.Button("Click for more BDO Assets"))
			Application.OpenURL("https://www.assetstore.unity3d.com/en/#!/search/page=1/sortby=popularity/query=publisher:11524");
		if(GUILayout.Button("Go to support forum"))
			Application.OpenURL(forumLink);
		if(GUILayout.Button("Visit the Developer Website"))
			Application.OpenURL("http://betodeoliveira.com");

		Footer();
		EndRectBox();
	}
}
