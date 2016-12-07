﻿using System.Runtime.InteropServices;
using Foundation;
using UIKit;

namespace IDTechSwiperBindingIOS
{
    //enums from SDK
    public enum UmRet
    {
        UMRET_SUCCESS,
        UMRET_NO_READER,
        UMRET_SDK_BUSY,
        UMRET_ALREADY_CONNECTED,
        UMRET_NOT_CONNECTED,
        UMRET_LOW_VOLUME,
        UMRET_UF_INVALID_STR,
        UMRET_UF_NO_FILE,
        UMRET_UF_INVALID_FILE

    };

    public enum UmTask
    {
        UMTASK_NONE,       //no async task running. SDK idle.
        UMTASK_CONNECT,    //connection task
        UMTASK_SWIPE,      //swipe task
        UMTASK_CMD,        //command task
        UMTASK_FW_UPDATE
    }

    public enum UmUfCode
    {
        UMUFCODE_SENDING_BLOCK = 21,
        UMUFCODE_VERIFYING_CHECKSUM = 30,
        UMUFCODE_RESENDING_BLOCK = 40,
        UMUFCODE_FAILED_TO_ENTER_BOOTLOADER_MODE = 303,
        UMUFCODE_FAILED_TO_SEND_BLOCK = 305,
        UMUFCODE_FAILED_TO_VERIFY_CHECKSUM = 306,
        UMUFCODE_CANCELED = 307
    }
}
