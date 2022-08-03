---------------------------------------------------------------------
-- U3DAvatar (C) CompanyName, All Rights Reserved
-- Created by: AuthorName
-- Date: 2022-07-28 14:50:19
---------------------------------------------------------------------

-- To edit this template in: Data/Config/Template.lua
-- To disable this template, check off menuitem: Options-Enable Template File

---@class ScrollRectModel
ScrollRectModel = {}
ScrollRectModel.datas={}

function  ScrollRectModel.setDatas(datas)
	ScrollRectModel.datas=datas
end

function ScrollRectModel.getDatasLength()
	return  #ScrollRectModel.datas
end

function  ScrollRectModel.getDataByIndex(index)
	if(index>=0 and index<#ScrollRectModel.datas) then
		return ScrollRectModel.datas[index+1]
	end
end

function ScrollRectModel.clearDatas()
	    ScrollRectModel.datasAndIndex={}
end

function ScrollRectModel.setScrollControl(scrollControl)
	ScrollRectModel.ScrollRectControl=scrollControl
end

function ScrollRectModel.DeleteDate(itemName)
		for k, v in pairs(ScrollRectModel.datas) do
			if(v==itemName) then
			 ScrollRectModel.datas[k]=nil
			 return  true	
			end
		end
	return  false
end
function isContanisValue(data,value)
	for k, v in pairs(data) do
		if v==value then
			return  true
		end
	end
	return  false
end

return  ScrollRectModel