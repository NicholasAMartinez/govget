import type { PingResult } from '../models/PingResult'

export async function getPing(): Promise<PingResult> {
  const response = await fetch('/api/ping')

  if (!response.ok) {
    throw new Error(`Ping failed with status ${response.status}`)
  }

  return response.json()
}
