using System.Collections.Generic;
using UnityEngine;

//�����桢�糡�����ݡ��־û����ݵ�
public class CacheManager : Singleton<CacheManager>
{
    private CharacterDefine _playerDefineCache;
    public Dictionary<int, GameObject> Objs;

    public void SetPlayerCache(CharacterDefine playerDefine)
    {
        _playerDefineCache = playerDefine;
    }
    public CharacterDefine GetPlayerCache()
    {
        CharacterDefine c = _playerDefineCache;
        _playerDefineCache = null;
        return c;
    }
}
