using System.Runtime.Serialization;
// left this unused using because it will be needed if you want to uncomment the code below for a collection property
using System.Collections.Generic;

namespace SimpleNHibernate
{
    // The DataContract tag marks this class as serializable - i.e. it can be parsed to/from JSON and seen by NHibernate
    [DataContract]
    public class Bodies
    {
        //The objects you'll be working with go in the Domain.  You should use the property names that map onto what they're called in the json
        //Then you can deserialise the json object straight into the .Net object
        // Only properties you tag as DataMember are available to JSON and the ORM
        
        // Common attributes
        [DataMember]
        public virtual int id { get; set; }
        [DataMember]
        public virtual int systemId { get; set; }
        [DataMember]
        public virtual string systemName { get; set; }
        [DataMember]
        public virtual string type { get; set; }
        [DataMember]
        public virtual string subType { get; set; }
        [DataMember]
        public virtual decimal distanceToArrival { get; set; }

        // Planet attributes
        [DataMember]
        public virtual bool isLandable { get; set; }
        [DataMember]
        public virtual decimal gravity { get; set; }
        [DataMember]
        public virtual decimal earthMasses { get; set; }
        [DataMember]
        public virtual decimal radius { get; set; }
        [DataMember]
        public virtual int surfaceTemperature { get; set; }
        [DataMember]
        public virtual decimal surfacePressure { get; set; }
        [DataMember]
        public virtual string volcanismType { get; set; }
        [DataMember]
        public virtual string atmosphereType { get; set; }

        // Star attributes
        [DataMember]
        public virtual bool isMainStar { get; set; }
        [DataMember]
        public virtual bool isScoopable { get; set; }
        [DataMember]
        public virtual string age { get; set; }
        [DataMember]
        public virtual string luminosity { get; set; }
        [DataMember]
        public virtual decimal absoluteMagnitude { get; set; }
        [DataMember]
        public virtual decimal solarMasses { get; set; }
        [DataMember]
        public virtual decimal solarRadius { get; set; }

        // Common attributes
        [DataMember]
        public virtual decimal orbitalPeriod { get; set; }
        [DataMember]
        public virtual decimal semiMajorAxis { get; set; }
        [DataMember]
        public virtual decimal orbitalEccentricity { get; set; }
        [DataMember]
        public virtual decimal orbitalInclination { get; set; }
        [DataMember]
        public virtual decimal argOfPeriapsis { get; set; }
        [DataMember]
        public virtual decimal rotationalPeriod { get; set; }
        [DataMember]
        public virtual bool rotationalPeriodTidallyLocked { get; set; }
        [DataMember]
        public virtual decimal axialTilt { get; set; }

        // If you have any collection properties then you create the objects and mapping for those seperately and add a virtual IList property
        // If you do then you will need to include a constructor which initialises any ILists to empty Lists
        // as well as a virtual void method that takes your child class and adds it to the list

        //[DataMember]
        //public virtual IList<Materials> Materials { get; set; }
        //[DataMember]
        //public virtual IList<Rings> Rings { get; set; }

        //public Bodies()
        //{
        //    Materials = new List<Materials>();
        //    Rings = new List<Rings>();
        //}

        //public virtual void AddMaterial(Materials toAdd)
        //{
        //    Materials.Add(toAdd);
        //}

        //public virtual void AddRing(Rings toAdd)
        //{
        //    Rings.Add(toAdd);
        //}
    }

    //[DataContract]
    //public class Materials
    //{
    //    [DataMember] // You want this one to point back to the parent record, it's not coming from the json, it's the key linking back to the body
    //    public virtual int parentid { get; set; }
    //    [DataMember]
    //    public virtual int material_id { get; set; }
    //    [DataMember]
    //    public virtual string material_name { get; set; }
    //    [DataMember]
    //    public virtual decimal share { get; set; }
    //}

    //[DataContract]
    //public class Rings
    //{
    //    [DataMember] // You want this one to point back to the parent record, it's not coming from the json, it's the key linking back to the body
    //    public virtual int parentid { get; set; } // 
    //    [DataMember]
    //    public virtual int id { get; set; }
    //    //[DataMember]
    //    //public virtual int created_at { get; set; }
    //    //[DataMember]
    //    //public virtual int updated_at { get; set; }
    //    [DataMember]
    //    public virtual string name { get; set; }
    //    [DataMember]
    //    public virtual decimal semi_major_axis { get; set; }
    //    [DataMember]
    //    public virtual int ring_type_id { get; set; }
    //    [DataMember]
    //    public virtual double ring_mass { get; set; }
    //    [DataMember]
    //    public virtual int ring_inner_radius { get; set; }
    //    [DataMember]
    //    public virtual int ring_outer_radius { get; set; }
    //    [DataMember]
    //    public virtual string ring_type_name { get; set; }
    //}
}