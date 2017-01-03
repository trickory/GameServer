import ('GameServerLib','LeagueSandbox.GameServer.Logic.Packets')
import ('GameServerLib','LeagueSandbox.GameServer.Core.Logic.PacketHandlers')      

logger = nil
game = nil       
playerManager = nil

function onServerStart(_game, _logger, _playerManager)   
  logger = _logger                        
  game = _game                          
  playerManager = _playerManager            
  logger:LogCoreInfo('onServerStart')
  game.PacketHandlerManager.OnHandleKeyCheck:Add(onHandleKeyCheck)     
  game.PacketHandlerManager.OnHandleQueryStatus:Add(onHandleQueryStatus)
  game.PacketHandlerManager.OnHandleLoadPing:Add(onHandleLoadPing)
end

function onHandleKeyCheck(peer, args)         
  local keyCheck = KeyCheck(args.Data)  
  local each = luanet.each
  local playerArray = playerManager:GetPlayers()    
  local playerNum = 0
  for p in each(playerArray) do
    local player = p.Item2
    if player.UserId == keyCheck.userId then
      if player.Peer ~= nil then
        if not player.IsDisconnected then
          logger:LogCoreWarning('Ignoring new player ' .. player.UserId .. ', already connected!');
          return
        end
      end               
      logger:LogCoreWarning('PlayerNo ' .. keyCheck.playerNo .. ' with userId ' .. keyCheck.userId);
      p.Item1 = peer.Adress.port;
      player.Peer = peer;
      local response = KeyCheck(keyCheck.userId, playerNum)         
      game.PacketHandlerManager:sendPacket(peer, response, Channel.CHL_HANDSHAKE)
      return
    end
    playerNum = playerNum + 1;
  end                 
end            

function onHandleQueryStatus(peer, args)           
  logger:LogCoreInfo('onHandleQueryStatus')    
  local response = QueryStatus()                  
  game.PacketHandlerManager:sendPacket(peer, response, Channel.CHL_S2C)
end

function onHandleLoadPing(peer, args)         
  logger:LogCoreInfo('onHandleLoadPing')
  local loadInfo = PingLoadInfo(args.Data)
  local peerInfo = playerManager:GetPeerInfo(peer)
  if peerInfo ~= nil then
    local response = PingLoadInfo(loadInfo, peerInfo.UserId)
    game.PacketHandlerManager:broadcastPacket(response, Channel.CHL_LOW_PRIORITY, PacketFlags.None)
  end
end