#######################
# general configuration

set history remove-duplicates unlimited
set disassembly-flavor intel

define hook-quit
    # kill
    set confirm off
end

define hook-kill
    set confirm off
end

########################
# specific configuration

#file bin/Mosa.HelloWorld.x86.bin
#target remote | Demos/unix/debug-helloworld-internal.sh
b *0x0

####################
# continue debugging

continue
