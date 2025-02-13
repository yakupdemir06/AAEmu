﻿using System;
using System.Collections.Concurrent;

namespace AAEmu.Game.Models.Game.Units;

public class UnitCooldowns
{
    public ConcurrentDictionary<uint, DateTime> Cooldowns { get; set; }

    public UnitCooldowns()
    {
        Cooldowns = new ConcurrentDictionary<uint, DateTime>();
    }

    public void AddCooldown(uint skillId, uint duration)
    {
        if (!Cooldowns.TryGetValue(skillId, out _))
            Cooldowns.TryAdd(skillId, DateTime.UtcNow + TimeSpan.FromMilliseconds(duration));
    }

    public bool CheckCooldown(uint skillId)
    {
        if (!Cooldowns.TryGetValue(skillId, out var endTime))
            return false;

        var timeLeft = endTime - DateTime.UtcNow;
        if (timeLeft > TimeSpan.FromMilliseconds(250))
            return true;

        RemoveCooldown(skillId);
        return false;
    }

    public void RemoveCooldown(uint skillId)
    {
        Cooldowns.TryRemove(skillId, out _);
    }
}
