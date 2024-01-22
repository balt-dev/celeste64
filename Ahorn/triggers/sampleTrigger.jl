module Celeste64SampleTrigger

using ..Ahorn, Maple

@mapdef Trigger "Celeste64/SampleTrigger" SampleTrigger(
    x::Integer, y::Integer, width::Integer=Maple.defaultTriggerWidth, height::Integer=Maple.defaultTriggerHeight,
    sampleProperty::Integer=0
)

const placements = Ahorn.PlacementDict(
    "Sample Trigger (Celeste64)" => Ahorn.EntityPlacement(
        SampleTrigger,
        "rectangle",
    )
)

end