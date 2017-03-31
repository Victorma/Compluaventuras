﻿using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

using uAdventure.Core;

namespace uAdventure.Editor
{
    public class ExitNewLinkTo : BaseChooseObjectPopup
    {

        public override void Init(DialogReceiverInterface e)
        {
            elements = Controller.getInstance().getSelectedChapterDataControl().getObjects<IChapterTarget>().ConvertAll(o => o.getId()).ToArray();
            selectedElementID = elements[0];

            base.Init(e);
        }

        void OnGUI()
        {
            EditorGUILayout.LabelField(TC.get("Exit.TargetScene"), EditorStyles.boldLabel);

            GUILayout.Space(20);

            selectedElementID = elements[EditorGUILayout.Popup(Array.IndexOf(elements, selectedElementID), elements)];

            GUILayout.Space(20);

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("OK"))
            {
                reference.OnDialogOk(selectedElementID, this);
                this.Close();
            }

            if (GUILayout.Button(TC.get("GeneralText.Cancel")))
            {
                reference.OnDialogCanceled();
                this.Close();
            }
            GUILayout.EndHorizontal();
        }
    }
}