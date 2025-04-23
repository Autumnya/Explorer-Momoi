using System.Collections.Generic;
using UnityEngine;

//管理缓存、跨场景数据、持久化数据等
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
