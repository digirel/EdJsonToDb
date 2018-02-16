using NHibernate.Mapping.ByCode.Conformist;
// left this unused using because it will be needed if you want to uncomment the Bag code below for a collection mapping
using NHibernate.Mapping.ByCode;

namespace SimpleNHibernate
{
    public class BodyMap : ClassMapping<Bodies>
    {
        // In the Mappings you map your object to your database model.  If the table is called TestObj and the columns are as the property names then it just works.
        // If you let NHibernate create the database then everything will match
        public BodyMap()
        {
            //If the table name didn't match you can set it - this is generally only useful if you've got a legacy database and the table name is a crap object name
            //Table("other");
            //    Id(x => x.Id);
            Id(x => x.id);
            // Common attributes
            Property(x => x.id);
            Property(x => x.systemId);
            Property(x => x.systemName);
            Property(x => x.type);
            Property(x => x.subType);
            Property(x => x.distanceToArrival);
            // Planet attributes
            Property(x => x.isLandable);
            Property(x => x.gravity);
            Property(x => x.earthMasses);
            Property(x => x.radius);
            Property(x => x.surfaceTemperature);
            Property(x => x.surfacePressure);
            Property(x => x.volcanismType);
            Property(x => x.atmosphereType);
            // Star attributes
            Property(x => x.isMainStar);
            Property(x => x.isScoopable);
            Property(x => x.age);
            Property(x => x.luminosity);
            Property(x => x.absoluteMagnitude);
            Property(x => x.solarMasses);
            Property(x => x.solarRadius);
            // Common attributes
            Property(x => x.orbitalPeriod);
            Property(x => x.semiMajorAxis);
            Property(x => x.orbitalEccentricity);
            Property(x => x.orbitalInclination);
            Property(x => x.argOfPeriapsis);
            Property(x => x.rotationalPeriod);
            Property(x => x.rotationalPeriodTidallyLocked);
            Property(x => x.axialTilt);
            //Property(x => x.materials); // has subset of values
            //Property(x => x.rings); // has subset of values

            // likewise if PropertyTwo had to map to a column called Bob for legacy reasons then...
            // Map(x => x.ProprtyTwo, columnName: "Bob");

            // If you have a collection property then you need a Bag.  E.g.
                //Bag(x => x.Materials, m => // TODO: Doesn't work anymore
                //    {
                //        //m.Key(k => k.Column("id"));
                //        m.Key(k => k.Column("parent_Id"));
                //        m.Cascade(Cascade.All.Include(Cascade.DeleteOrphans));
                //        m.Lazy(CollectionLazy.NoLazy); // laziness determines whether all the child records will automatically be read from the database when you load the parent object or whether it will wait until some code tries to access them and only load at that point
                //    }
                //    , m => m.OneToMany()
                //);

                //Bag(x => x.Rings, r => // TODO: Doesn't work anymore
                //{
                //        //m.Key(k => k.Column("id"));
                //        r.Key(k => k.Column("parent_Id"));
                //        r.Cascade(Cascade.All.Include(Cascade.DeleteOrphans));
                //        r.Lazy(CollectionLazy.NoLazy); // laziness determines whether all the child records will automatically be read from the database when you load the parent object or whether it will wait until some code tries to access them and only load at that point
                //    }
                //    , r => r.OneToMany()
                //);
        }
    }

    //public class MaterialMap : ClassMapping<Materials>
    //{
    //    public MaterialMap()
    //    {
    //        Id(x => x.material_id);
    //        Property(x => x.material_name);
    //        Property(x => x.share);
    //    }
    //}

    //public class RingMap : ClassMapping<Rings>
    //{
    //    public RingMap()
    //    {
    //        Id(x => x.id);
    //        //Property(x => x.created_at);
    //        //Property(x => x.updated_at);
    //        Property(x => x.name);
    //        Property(x => x.semi_major_axis);
    //        Property(x => x.ring_type_id);
    //        Property(x => x.ring_type_name);
    //        Property(x => x.ring_mass);
    //        Property(x => x.ring_inner_radius);
    //        Property(x => x.ring_outer_radius);
    //    }
    //}
}