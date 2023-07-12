using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDK1;

namespace PedigreeMF.PedigreeClasses;
internal class Entity {
    public string Id {
        get; set;
    }
    public int BirthYear {
        get; set;
    }
    public string Death {
        get; set;
    }
    public string Sex {
        get; set;
    }
    public string? Mother {
        get; set;
    }
    public ArrayList Fathers {
        get; set;
    }
    public Color FigColor {
        get; set;
    }

    public override string ToString() {
        return "id " + Id + ", BirthYear " + BirthYear + ", Death " + Death + ", Sex " + Sex + ", Mother " + Mother + " Fathers (" + string.Join(",", (string[])Fathers.ToArray(typeof(string))) + "), Color " + FigColor.ToString(); 
    }
}
