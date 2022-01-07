module Ingredients

open Units
open References

type Measure = 
    | Unspecified                   // no quantity specified at all, e.g. "olive oil" or "vanilla extract"
    | SmallAmount of string option  // any small amount, optionall with specified description string e.g. "bit of ..." or "some ..."
    | Quantity of SimpleQuantity    // amount with value with unit (can be number), e.g. "200g" or "3". Can be approximate, e.g. "~100mL"
    | Range of RangedQuantity       // lower and upper value with unit. cannot be approximate
and SimpleQuantity = 
    {
        Amount:         float
        Unit:           Unit 
        IsApproximate:  bool 
    }
and RangedQuantity  = 
    {
        Lower:          float 
        Upper:          float 
        Unit:           Unit 
    }


type Ingredient = 
    { 
        PrimaryOption:  SimpleIngredient
        OtherOptions:   SimpleIngredient list 
        IsOptional:     bool 
        Notes:          Reference list
    }
and SimpleIngredient = 
    {
        Item:       string
        Measure:    Measure
        Prep:       string option
    }


let createQuantity (isApprox: bool) (value: float) (unitSymbol: string) = 
    let unit = Unit.create unitSymbol 
    Quantity { 
        Amount = value
        Unit = unit 
        IsApproximate = isApprox
    }

let createRange (lower: float) (upper: float) (unitSymbol: string) = 
    let unit = Unit.create unitSymbol 
    Range {
        Lower = lower 
        Upper = upper
        Unit = unit
    }

