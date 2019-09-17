
using System;
public class Packet : IDisposable {
    public int ptr;
    public int length;
    public byte[] data;
    public Packet (byte[] array) : this (array, array.Length) {

    }

    public Packet (byte[] array, int length) {
        ptr = 0;
        data = array;
        length = length << 3;
    }

    public void Dispose(){

    }

    public void WriteByteAtPtr (byte value, int bits) {

        if (bits > 0) {
            value = (byte) ((int) value & 255 >> 8 - bits);

            int byteIndex = ptr >> 3; //准备写入的byteIndex

            int num = ptr & 7; //byteIndex对应的byte已经写了多少位

            int num2 = 8 - num; //byteIndex对应的byte剩余多少位可写

            int num3 = num2 - bits; //写完以后剩余位数

            if (num3 >= 0) {
                //如果空间足够,就在data[num]写入value
                int num4 = 255 >> num2 | 255 << 8 - num3;

                data[byteIndex] = (byte) (((int) data[byteIndex] & num4) | (int) value << num);
            } else {
                //如果空间不够,就将value拆分,放入两个字节中
                data[byteIndex] = (byte) (((int) data[byteIndex] & 255 >> num2) | (int) value << num);

                data[byteIndex + 1] = (byte) (((int) data[byteIndex + 1] & 255 << bits - num2) | value >> num2);
            }
        }

        ptr += bits; //ptr增加

    }

    public bool WriteBool (bool value) {
        WriteByteAtPtr ((byte) (value ? 1 : 0), 1);
        return value;
    }

    public void WriteByte (byte value, int bits = 8) {
        WriteByteAtPtr (value, bits);
    }

    public void WriteUShort (ushort value, int bits = 16) {
        if (bits <= 8) {
            WriteByteAtPtr ((byte) (value & 255), bits);
        } else {
            WriteByteAtPtr ((byte) (value & 255), 8);
            WriteByteAtPtr ((byte) (value >> 8), bits - 8);
        }
    }

    private byte ReadByteAtPtr (int bits) {
        byte result;
        if (bits <= 0) {
            result = 0;
        } else {
            int num = ptr >> 3;
            int num2 = ptr % 8;
            byte b;

            if (num2 == 0 && bits == 8) {
                b = data[num];
            } else {
                int num3 = data[num] >> num2;

                int num4 = bits - (8 - num2);

                if (num4 < 1) {
                    b = (byte) (num3 & 255 >> 8 - bits);
                } else {
                    int num5 = (int) data[num + 1] & 255 >> 8 - num4;

                    b = (byte) (num3 | num5 << bits - num4);
                }
            }
            ptr += bits;
            result = b;
        }
        return result;
    }
}