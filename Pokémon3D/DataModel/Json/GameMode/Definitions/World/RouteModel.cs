﻿using System.Runtime.Serialization;

// Disable Code Analysis for warning CS0649: Field is never assigned to, and will always have its default value.
#pragma warning disable 0649

namespace Pokémon3D.DataModel.Json.GameMode.Definitions.World
{
    /// <summary>
    /// The data model for a route object.
    /// </summary>
    [DataContract]
    class RouteModel : MapObjectModel
    {
        #region RouteType

        [DataMember(Order = 4, Name = "RouteType")]
        private string _routeType;
        
        public RouteType RouteType
        {
            get { return ConvertStringToEnum<RouteType>(_routeType); }
        }

        #endregion

        #region RouteDirection

        [DataMember(Order = 5, Name = "RouteDirection")]
        private string _routeDirection;
        
        public RouteDirection RouteDirection
        {
            get { return ConvertStringToEnum<RouteDirection>(_routeDirection); }
        }

        #endregion
    }
}
