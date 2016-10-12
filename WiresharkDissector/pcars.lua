-- trivial protocol example
-- declare our protocol
pcars_proto = Proto("pcars","PCars Telemetry Data")
-- create a function to dissect it
function trivial_proto.dissector(buffer,pinfo,tree)
    pinfo.cols.protocol = "PCARS"
    local subtree = tree:add(pcars_proto,buffer(),"PCars Telemetry Data")
    subtree:add(buffer(0,2),"Build Version Number: " .. buffer(0,2):uint())
    subtree:add(buffer(2,4),"Packet Type: " .. buffer(0,2):uint())
    
   -- subtree = subtree:add(buffer(2,2),"The next two bytes")
    -- subtree:add(buffer(2,1),"The 3rd byte: " .. buffer(2,1):uint())
    -- subtree:add(buffer(3,1),"The 4th byte: " .. buffer(3,1):uint())
end
-- load the udp.port table
udp_table = DissectorTable.get("udp.port")
-- register our protocol to handle udp port 7777
udp_table:add(5606,pcars_proto)