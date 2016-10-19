-- trivial dissector for Project Cars telemetry data 
-- being sent out across the UDP stream
-- declare our protocol
pcars_proto = Proto("pcars","PCars Telemetry Data")

-- create a function to dissect it
function pcars_proto.dissector(buffer,pinfo,tree)
    pinfo.cols.protocol = "PCARS"
    
    local subtree = tree:add(pcars_proto,buffer(),"PCars Telemetry Data")


    local getAsInt =    function(range,name)
                            subtree:add(range, name ..": " .. range:le_int())
                        end

    local getAsUint =   function(range,name)
                            subtree:add(range, name .. ": " .. range:le_uint())
                        end

    local getAsFloat =  function(range,name)
                            subtree:add(range, name .. ": " .. range:le_float())
                        end


    getAsUint(buffer(0,2),"Build Version Number")
    getAsUint(buffer(2,1),"Packet Type") 

    getAsUint(buffer(6,1),"Throttle")
    getAsUint(buffer(7,1),"Brake")
    getAsInt(buffer(8,1),"Steering")
    getAsUint(buffer(9,1),"Clutch")


    getAsInt(buffer(100,2),"Oil Temperature (C)")
    getAsInt(buffer(104,2),"Water Temperature (C)")
    getAsUint(buffer(102,2),"Oil Pressure (kPa)")
    getAsInt(buffer(106,2),"Water Pressure (kPa)")
    getAsUint(buffer(131,1),"Crash state")

    getAsFloat(buffer(116,4), "Fuel level")
    getAsFloat(buffer(120,4), "Speed")
    
end

-- load the udp.port table
udp_table = DissectorTable.get("udp.port")
-- register our protocol to handle udp port 7777
udp_table:add(5606,pcars_proto)



-- // Car state
--   s16   sOilTempCelsius;              // 100
--   u16   sOilPressureKPa;              // 102
--   s16   sWaterTempCelsius;            // 104
--   u16   sWaterPressureKpa;            // 106
--   u16   sFuelPressureKpa;             // 108
--   u8    sCarFlags;                    // 110
--   u8    sFuelCapacity;                // 111
--   u8    sBrake;                       // 112
--   u8    sThrottle;                    // 113
--   u8    sClutch;                      // 114
--   s8    sSteering;                    // 115
--   f32   sFuelLevel;                   // 116
--   f32   sSpeed;                       // 120
--   u16   sRpm;                         // 124
--   u16   sMaxRpm;                      // 126
--   u8    sGearNumGears;                // 128
--   u8    sBoostAmount;                 // 129
--   s8    sEnforcedPitStopLap;          // 130
--   u8    sCrashState;                  // 131

--   f32   sOdometerKM;                  // 132
--   f32   sOrientation[3];              // 136
--   f32   sLocalVelocity[3];            // 148
--   f32   sWorldVelocity[3];            // 160
--   f32   sAngularVelocity[3];          // 172
--   f32   sLocalAcceleration[3];        // 184
--   f32   sWorldAcceleration[3];        // 196
--   f32   sExtentsCentre[3];            // 208
