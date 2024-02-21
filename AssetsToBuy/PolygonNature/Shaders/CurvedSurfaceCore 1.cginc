
float3 _Curvature;
float _Distance;

void vert(inout appdata_full v)
{				
	float4 vPos = mul( unity_ObjectToWorld, v.vertex);

	float dist = 0; 
	//dist = vPos.z - _WorldSpaceCameraPos.z;
	dist = distance(vPos.z , _WorldSpaceCameraPos.z) - _Distance;
	//if( dist <=1)	{	dist = 1;	}		
	
	float addY = sin(0.05*dist);
	
	vPos.y += addY * _Curvature.y;

	dist = vPos.x - _WorldSpaceCameraPos.x;
	float addHY = dist * dist;
	vPos.y -= addHY* _Curvature.x ;
	
	// for corner
	vPos.x += addY * _Curvature.z;

	vPos = mul ( unity_WorldToObject, vPos);
	v.vertex = vPos;
}	