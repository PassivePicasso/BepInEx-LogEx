
#  WebSlog 

#### A ProtoBuf powered WebSocket Log forwarder for BepInEx


---

This is a patcher time mod for BepInEx which provides a simple WebSocket Server that forward's 
bepinex log messages to connected WebSockets on the local machine.

This is primarily intended to provide the ability to create alternative log display systems for bepinex.

WebSlog forwards messages over Websockets using protobuf-net , an implementation of Google's Protobuf.

The protobuf definition for log messages is as follows;

```cs
    [ProtoContract]
    public class LogEntry
    {
        [ProtoMember(1)]
        public string Source { get; internal set; }

        [ProtoMember(2)]
        public string Level { get; internal set; }

        [ProtoMember(3)]
        public int LevelCode { get; internal set; }

        [ProtoMember(4)]
        public string Data { get; internal set; }
    }
```


---


### Change Log


##### 3.0.2 / 3.0.3 / 3.0.4


* Update this description to fix formatting errors


##### 3.0.1


* Update README.md with change log and protobuf definition


##### 3.0.0

* Migrate to using Protobuf for websocket protocol


##### 2.2.2

* Attempt to use Newtonsoft because Manual json is error prone and I'm stupid for trying it.

##### 2.2.1


* Fix manual JSON setup


##### 2.2.0


* Migrate off JsonUtility due to compatibility issues, implement simple manual JSON.


##### 2.1.0


* Migrate to using JsonUtiliy


##### 1.1.0


* Cache logs and forward cached logs to clients upon connect


##### 1.0.0


* Initial Release

