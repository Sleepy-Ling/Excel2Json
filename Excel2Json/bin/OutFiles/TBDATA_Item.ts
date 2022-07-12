export class TBDATA_Item {
    /*-----property start-----*/	/**类型 */	public ID: string;
	/**名字 */	public name: string;
	/**能踩在该物品的id */	public canStayID: string;
	/**蛇能否食用 */	public eatable: boolean;
	/**蛇能否通过 */	public canPassThrough: boolean;
	/**是否能推动 */	public canPush: boolean;
	/**是否受重力影响 */	public hasGravity: boolean;
	/**触碰时触发事件 */	public triggerEventID: number;
	/**预制体名称 */	public prefabName: string;

    /*-----property end-----*/

}
