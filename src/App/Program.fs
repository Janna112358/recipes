open System
open RecipesDSL

let lentils = Ingredients.SingleOption {
        Item = "red lentils (dried)"
        Measure = Some {
            Quantity        = Ingredients.Quantity 200.0
            Unit            = Units.Unit.U "g" "gram"
            IsApproximate   = true
        }
        IsOptional  = false
        Prep        = None
        Notes       = []
    }

let optionalFat = Ingredients.MultipleOptions [
    {
        Item = "vegan butter"
        // to do: extend Measure so it can also be a descriptive string (like "a bit")
        Measure = Some { 
            Quantity        = Ingredients.Quantity 1.0 
            Unit            = Units.Unit.U "bit" "bit"
            IsApproximate   = false
        }
        IsOptional = true 
        Prep = None 
        Notes = []
    }
    {
        Item = "coconut oil"
        Measure = Some { 
            Quantity        = Ingredients.Quantity 1.0 
            Unit            = Units.Unit.U "bit" "bit"
            IsApproximate   = false
        }
        IsOptional = true 
        Prep = None 
        Notes = []
    }
]

[<EntryPoint>]
let main args =
    printfn "%s" <| Printers.printIngredient lentils
    printfn "%s" <| Printers.printIngredient optionalFat
    0 // return an integer exit code
