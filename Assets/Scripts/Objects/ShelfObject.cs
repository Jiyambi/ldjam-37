﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfObject : MonoBehaviour 
{
	[SerializeField]
	private string m_productID;
	[SerializeField]
	private List<Transform> m_productSlots = new List<Transform>();

	private List<ProductObject> m_productStock = new List<ProductObject>();

	public string productID { get { return m_productID; }}

	// Use this for initialization
	void Start () {
		AddOrRemoveStock(m_productSlots.Count); // FULLY STOCK AT START
	}

	public void SetupForProduct(string _productID)
	{
		m_productID = _productID;

		// Update ProductObjects in inventory
		int numItems = m_productStock.Count;
		AddOrRemoveStock(-numItems);
		AddOrRemoveStock(numItems);

		// TODO: Update sign
	}

	public void AddOrRemoveStock(int _iStock)
	{
		if (_iStock > 0)
		{
			// Instantiate a product object and add it to the stock
			for (int i = 0; i < _iStock; ++i)
			{
				if (m_productStock.Count + 1 > m_productSlots.Count)
				{
					Debug.LogError("Tried to add more stock than there was room for!");
					return;
				}

				ProductObject newObject = ProductObject.Create(m_productID);
				newObject.transform.SetParent(transform);
				newObject.transform.position = m_productSlots[m_productStock.Count].transform.position;
				m_productStock.Add(newObject);
			}
		}
		else if (_iStock < 0)
		{
			for (int i = 0; i < _iStock; ++i)
			{
				if (m_productStock.Count == 0)
				{
					Debug.LogError("Tried to remove more stock than remained on shelf!");
					return;
				}

				Destroy(m_productStock[m_productStock.Count - 1].gameObject);
				m_productStock.RemoveAt(m_productStock.Count - 1);
			}
		}
	}


}
