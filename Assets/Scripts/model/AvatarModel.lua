---------------------------------------------------------------------
-- U3DAvatar (C) CompanyName, All Rights Reserved
-- Created by: AuthorName
-- Date: 2022-07-28 10:24:04
---------------------------------------------------------------------

-- To edit this template in: Data/Config/Template.lua
-- To disable this template, check off menuitem: Options-Enable Template File

---@class AvatarModel
AvatarModel = {}
AvatarModel.part={}
AvatarModel.num={}
AvatarModel.state=""

function AvatarModel.updateData(part,num)
	local len=#AvatarModel.part
	for i = 1, len do
		if AvatarModel.part[i]==part then
			AvatarModel.num[i]=num
			UpdatePart(part,num)
			return
		end
	end
end
function  AvatarModel.setState(state)
	AvatarModel.state=state	
end

function  AvatarModel.initCharacter()
	local part=AvatarModel.control.dataPart
	local num=AvatarModel.control.dataNum
	configCharaterPart(part,num)
	for i = 1, #AvatarModel.part do
		UpdatePart(AvatarModel.part[i],AvatarModel.num[i])
	end
end

function AvatarModel.setControl(control)
	AvatarModel.control=control
end

function configCharaterPart(part,num)
	local len=#part
	for i = 1, len do
		AvatarModel.part[i]=part[i]
		AvatarModel.num[i]=num[i]
		configRolePart(part[i])
	end
end
function configRolePart(part)
	
	AvatarModel.control.AddNewPart(part)
end

function UpdatePart(part,num)
	AvatarModel.control.onUpdatePart(AvatarModel.state,part,num)
end

return  AvatarModel
