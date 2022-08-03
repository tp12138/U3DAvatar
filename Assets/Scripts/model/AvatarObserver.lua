AvatarObserver={}
function AvatarObserver:new(o)
	o=o or {}
	setmetatable(o,self)
	self._index=self
	return o
end

--onUpdatePart
function AvatarObserver:onUpdatePart(state,part,num)
	print("in the state :"..state..",the part: "..part..",update to :"..num)
end

function AvatarObserver:AddNewPart(part)
	print("add part:"..part)
end
