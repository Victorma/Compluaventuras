﻿using RAGE.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TrackerExtension {

    /**********************
     * Static attributes
     * *******************/

    private static ExtensionTransformations TR;
    private static MovementTracker MT;
    

    /**********************
     * Extension enums and classes
     * *******************/
    public enum Verb
    {
        Moved
    }

    public enum Extension
    {
        Geopoint
    }

    // Transformations
    public class ExtensionTransformations
    {
        private Dictionary<string, string> verbIds = new Dictionary<string, string>()
        {
            { Verb.Moved.ToString().ToLower(), "https://w3id.org/xapi/seriousgames/verbs/moved"}
        };

        private Dictionary<string, string> objectIds = new Dictionary<string, string>()
        {
            { MovementTracker.Movement.Geoposition.ToString().ToLower(), "https://custom/geoposition" }
        };
    }

    /// <summary>
    /// Movement tracker class used to extend the normal tracker
    /// </summary>
    public class MovementTracker : Tracker.IGameObjectTracker
    {

        private Tracker tracker;

        public void setTracker(Tracker tracker)
        {
            this.tracker = tracker;
        }


        /* MOVEMENTS */

        public enum Movement
        {
            Geoposition
        }

        /// <summary>
        /// Player accessed a reachable.
        /// Type = Accessible 
        /// </summary>
        /// <param name="id">Reachable identifier.</param>
        public void Geoposition(string id, Vector2 latLon)
        {
            tracker.setGeopoint(latLon.x, latLon.y);
            tracker.ActionTrace(Verb.Moved.ToString().ToLower(), Movement.Geoposition.ToString().ToLower(), id);
        }

    }

    /**********************
     * Static methods
     * **********************/

    /// <summary>
    /// This method allows to set a Geopoint as extension to the current trace.
    /// </summary>
    /// <param name="t">tracker to use</param>
    /// <param name="lat">latitude</param>
    /// <param name="lon">longitude</param>
    public static void setGeopoint(this Tracker t, float lat, float lon)
    {
        if (TR == null)
            TR = new ExtensionTransformations();

        t.setVar(Extension.Geopoint.ToString().ToLower(), lat + "," + lon);
    }

    /// <summary>
    /// Movement tracker static accessor
    /// </summary>
    public static MovementTracker Movement
    {
        get
        {
            if (MT == null)
            {
                MT = new MovementTracker();
                MT.setTracker(Tracker.T);
            }

            return MT;
        }
    }

    
}
