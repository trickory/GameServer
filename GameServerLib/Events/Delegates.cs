using System;
using LeagueSandbox.GameServer.Events.Args;
using LeagueSandbox.GameServer.NewGameObjects;

namespace LeagueSandbox.GameServer.Events
{
    public delegate void AIHeroApplyCooldown(AIHeroClient sender, OnHeroApplyCooldownEventArgs args);
    public delegate void AIHeroClientDeath(Obj_AI_Base sender, OnHeroDeathEventArgs args);
    public delegate void AIHeroClientSpawn(Obj_AI_Base sender);
    public delegate void AttackableUnitDamage(AttackableUnit sender, AttackableUnitDamageEventArgs args);
    public delegate void AttackableUnitModifyShield(AttackableUnit sender, AttackableUnitModifyShieldEventArgs args);
    public delegate void AudioManagerPlaySound(AudioManagerPlaySoundEventArgs args);
    public delegate void CameraSnap(CameraSnapEventArgs args);
    public delegate void CameraToggleLock(CameraLockToggleEventArgs args);
    public delegate void CameraUpdate(CameraUpdateEventArgs args);
    public delegate void CameraZoom(CameraZoomEventArgs args);
    public delegate void GameAfk(GameAfkEventArgs args);
    public delegate void GameEnd(GameEndEventArgs args);
    public delegate void GameNotify(GameNotifyEventArgs args);
    public delegate void GameObjectCreate(GameObject sender, EventArgs args);
    public delegate void GameObjectDelete(GameObject sender, EventArgs args);
    public delegate void GamePostTick(EventArgs args);
    public delegate void GamePreTick(EventArgs args);
    public delegate void GameProcessPacket(GamePacketEventArgs args);
    public delegate void GameSendPacket(GamePacketEventArgs args);
    public delegate void GameTick(EventArgs args);
    public delegate void GameUpdate(EventArgs args);
    public delegate void Obj_AI_BaseBuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainEventArgs args);
    public delegate void Obj_AI_BaseBuffLose(Obj_AI_Base sender, Obj_AI_BaseBuffLoseEventArgs args);
    public delegate void Obj_AI_BaseBuffUpdate(Obj_AI_Base sender, Obj_AI_BaseBuffUpdateEventArgs args);
    public delegate void Obj_AI_BaseDoCastSpell(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args);
    public delegate void Obj_AI_BaseLevelUp(Obj_AI_Base sender, Obj_AI_BaseLevelUpEventArgs args);
    public delegate void Obj_AI_BaseNewPath(Obj_AI_Base sender, GameObjectNewPathEventArgs args);
    public delegate void Obj_AI_BaseOnBasicAttack(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args);
    public delegate void Obj_AI_BaseOnSurrenderVote(Obj_AI_Base sender, Obj_AI_BaseSurrenderVoteEventArgs args);
    public delegate void Obj_AI_BasePlayAnimation(Obj_AI_Base sender, GameObjectPlayAnimationEventArgs args);
    public delegate void Obj_AI_BaseTeleport(Obj_AI_Base sender, GameObjectTeleportEventArgs args);
    public delegate void Obj_AI_ProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args);
    public delegate void Obj_AI_UpdateModel(Obj_AI_Base sender, UpdateModelEventArgs args);
    public delegate void Obj_AI_UpdatePosition(Obj_AI_Base sender, Obj_AI_UpdatePositionEventArgs args);
    public delegate void HeroPostIssueOrder(Obj_AI_Base sender, HeroIssueOrderEventArgs args);
    public delegate void Hero_DoEmote(AIHeroClient sender, HeroDoEmoteEventArgs args);
    public delegate void Hero_ProcessIssueOrder(Obj_AI_Base sender, HeroIssueOrderEventArgs args);
    public delegate void Hero_SwapItem(AIHeroClient sender, HeroSwapItemEventArgs args);
    public delegate void ShopBuyItem(AIHeroClient sender, ShopActionEventArgs args);
    public delegate void ShopSellItem(AIHeroClient sender, ShopActionEventArgs args);
    public delegate void SpellbookCastSpell(Spellbook sender, SpellbookCastSpellEventArgs args);
    public delegate void SpellbookPostCastSpell(Spellbook sender, SpellbookCastSpellEventArgs args);
    public delegate void SpellbookStopCast(Obj_AI_Base sender, SpellbookStopCastEventArgs args);
    public delegate void SpellbookUpdateChargeableSpell(Spellbook sender, SpellbookUpdateChargeableSpellEventArgs args);

}
