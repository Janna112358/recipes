module Ingredients

open Units

type Measure = 
    {
        Quantity:       Quantity 
        Unit:           Unit
        IsApproximate:  bool
    }
and Quantity = 
    | Quantity of float 
    | Range of float * float

type Ingredient = 
    | SingleOption of SimpleIngredient 
    | MultipleOptions of SimpleIngredient list
and SimpleIngredient = 
    {
        Item:       string
        Measure:    Measure option
        IsOptional: bool
        Prep:       string option
        Notes:      string list
    }
                
            